using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AcheoOnibus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        public Geopoint posicao { get; set; }

        string itinerario;
        int sentidoViagem;
        bool mostraUsuario = true;
        List<Onibus> listaOnibus = new List<Onibus>();
        DispatcherTimer contador = new DispatcherTimer();

        public MapPage()
        {
            this.InitializeComponent();
        }

        public Geopoint position { get; set; }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            List<object> listaDeParametros = e.Parameter as List<object>;

            if (listaDeParametros != null && listaDeParametros.Count > 1)
            {
                itinerario = listaDeParametros[0].ToString();
                sentidoViagem = Convert.ToInt32(listaDeParametros[1].ToString());
            }

            RecuperaPosicaoAtual();
        }

        private async void ExibirMensagem(string titulo, string mensagem)
        {
            ContentDialog d = new ContentDialog();
            d.Title = titulo;
            d.Content = mensagem;
            d.PrimaryButtonText = "Ok";
            await d.ShowAsync();
        }

        private void getPosicaoAtualOnibus(object sender, object e)
        {
            listaOnibus.Clear();
            try
            {
                HttpClient cliente = new HttpClient();
                HttpResponseMessage resposta = cliente.GetAsync("http://localhost:1916/api/Onibus").Result;
                HttpContent stream = resposta.Content;
                var resultadoLista = stream.ReadAsStringAsync();
                List<Onibus> listOnibus = JsonConvert.DeserializeObject<List<Onibus>>(resultadoLista.Result);

                if (listOnibus == null)
                {
                    contador.Stop();
                    throw new ArgumentException("Erro ao receber dados!");
                }

                foreach (Onibus onibus in listOnibus)
                {
                    if (onibus.numero == itinerario && onibus.sentidoViagem == sentidoViagem)
                    {
                        listaOnibus.Add(onibus);
                    }
                }

                if (listOnibus == null)
                {
                    contador.Stop();
                    throw new ArgumentException("Erro! Ônibus não encontrado para o itinerário e sentido de viagem informados!");
                }

                foreach (Onibus onibus in listaOnibus)
                {
                    mostraUsuario = false;
                    posicao = new Geopoint(new BasicGeoposition() { Latitude = Convert.ToDouble(onibus.latitude), Longitude = Convert.ToDouble(onibus.longitude) });
                    MostrarPosicao(posicao);
                }

            }
            catch (Exception err)
            {
                //ExibirMensagem("Erro!", err.Message);
                Frame.Navigate(typeof(MainPage), err.Message);
            }
        }

        private void AddMapIcon(Geopoint point)
        {
            MapIcon icon = new MapIcon();
            icon.Location = point;
            icon.NormalizedAnchorPoint = new Point(0.0, 0.0);
            
            if (mostraUsuario)
            {
                icon.Title = "Eu";
                //icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/user_1.png"));
                icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/user_2.png"));
            }
            else
            {
                icon.Title = itinerario;
                icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/bus.png"));
            }

            mapFindBus.MapElements.Add(icon);
            //mapFindBus.MapElements.Remove(icon);
        }

        private async void MostrarPosicao(Geopoint posicao)
        {
            //position = new Geopoint(new BasicGeoposition() { Latitude = -15.823127, Longitude = -47.9208744 });
            //position = new Geopoint(new BasicGeoposition() { Latitude = location.latitude, Longitude = location.longitude });
            try
            {
                //mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.AerialWithRoads;
                mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.Road;

                AddMapIcon(posicao);

                await mapFindBus.TrySetViewAsync(posicao, 18.0, 0, 0, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void RecuperaPosicaoAtual()
        {
            Geolocator g = new Geolocator();
            Geoposition gp = await g.GetGeopositionAsync();
            posicao = new Geopoint(new BasicGeoposition() { Latitude = gp.Coordinate.Point.Position.Latitude, Longitude = gp.Coordinate.Point.Position.Longitude });
            MostrarPosicao(posicao);
            iniciarContador();
        }

        private void iniciarContador()
        {
            contador.Tick += getPosicaoAtualOnibus;
            contador.Interval = new TimeSpan(0, 0, 5);
            contador.Start();
        }

    }
}
