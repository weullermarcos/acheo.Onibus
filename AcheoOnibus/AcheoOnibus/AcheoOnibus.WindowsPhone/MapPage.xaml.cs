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
        const double SLIDER_ZOOM_INICIAL = 18.0;
        const int INTERVAL_HOR = 0;
        const int INTERVAL_MIN = 0;
        const int INTERVAL_SEG = 5;
        const double DELAY_SEG = 2;

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

            recuperaPosicaoAtual();
        }

        private void sldEixoY_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sldEixoY != null)
            {
                MostrarPosicao(posicao);
            }
        }

        private void sldEixoX_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sldEixoX != null)
            {
                MostrarPosicao(posicao);
            }
        }

        private void sldZoom_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sldZoom != null)
            {
                MostrarPosicao(posicao);
            }
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
                    AddMapIcon(posicao);
                }

                MostrarPosicao(posicao);
            }
            catch (Exception err)
            {
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
                icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/user_2.png"));
            }
            else
            {
                icon.Title = itinerario;
                icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/bus.png"));
            }

            mapFindBus.MapElements.Add(icon);
        }

        private async void MostrarPosicao(Geopoint posicao)
        {
            try
            {
                mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.Road;
                await mapFindBus.TrySetViewAsync(posicao, sldZoom.Value, sldEixoX.Value, sldEixoY.Value, MapAnimationKind.Bow);
                //await mapFindBus.Focus();
            }
            catch (Exception)
            {
                Frame.Navigate(typeof(MainPage), "Erro! Não foi possível mostrar a posição!");
            }
        }

        private async void sleep(double seconds)
        {
            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(seconds));
        }  
        
        private void iniciarContador()
        {
            contador.Tick += getPosicaoAtualOnibus;
            contador.Interval = new TimeSpan(INTERVAL_HOR, INTERVAL_MIN, INTERVAL_SEG);
            contador.Start();
        }

        private async void recuperaPosicaoAtual()
        {
            Geolocator g = new Geolocator();
            Geoposition gp = await g.GetGeopositionAsync();
            posicao = new Geopoint(new BasicGeoposition() { Latitude = gp.Coordinate.Point.Position.Latitude, Longitude = gp.Coordinate.Point.Position.Longitude });
            
            mapFindBus.Center = posicao;

            sldZoom.Value = SLIDER_ZOOM_INICIAL;

            AddMapIcon(posicao);
            MostrarPosicao(posicao);

            sleep(DELAY_SEG);

            iniciarContador();
        }
    }
}
