﻿using Newtonsoft.Json;
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
        public Geopoint centroAtualDoMapa { get; set; }
        public MapIcon iconeAntigo { get; set; }


        string itinerario;
        int sentidoViagem;
        bool mostraUsuario = true;
        List<Onibus> listaFiltradaOnibus = new List<Onibus>();
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

        private void getPosicaoAtualOnibus(object sender, object e)
        {
            listaFiltradaOnibus.Clear();
            try
            {
                HttpClient cliente = new HttpClient();
                HttpResponseMessage resposta = cliente.GetAsync("http://localhost:1916/api/Onibus").Result;
                HttpContent stream = resposta.Content;
                var resultadoLista = stream.ReadAsStringAsync();
                List<Onibus> listaTodosOnibus = JsonConvert.DeserializeObject<List<Onibus>>(resultadoLista.Result);

                if (listaTodosOnibus == null)
                {
                    contador.Stop();
                    throw new ArgumentException("Erro ao receber dados!");
                }

                foreach (Onibus onibus in listaTodosOnibus)
                {
                    if (onibus.numero == itinerario && onibus.sentidoViagem == sentidoViagem)
                    {
                        listaFiltradaOnibus.Add(onibus);
                    }
                }

                if (listaFiltradaOnibus == null)
                {
                    contador.Stop();
                    throw new ArgumentException("Erro! Ônibus não encontrado para o itinerário e sentido de viagem informados!");
                }

                foreach (Onibus onibus in listaFiltradaOnibus)
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
            MapIcon novoIcone = new MapIcon();
            novoIcone.Location = point;
            novoIcone.NormalizedAnchorPoint = new Point(0.0, 0.0);
            
            if (mostraUsuario)
            {
                novoIcone.Title = "Eu";
                novoIcone.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/user_2.png"));
            }
            else
            {
                novoIcone.Title = itinerario;
                novoIcone.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/bus.png"));
            }

            if (iconeAntigo != null || mostraUsuario == false)
            {
                mapFindBus.MapElements.Remove(iconeAntigo);
                iconeAntigo = novoIcone;
            }

            mapFindBus.MapElements.Add(novoIcone);

        }

        private async void MostrarPosicao(Geopoint posicao)
        {
            try
            {
                if (centroAtualDoMapa == null)
                {
                    centroAtualDoMapa = posicao;
                }

                mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.Road;
                //await mapFindBus.TrySetViewAsync(posicao, sldZoom.Value, sldEixoX.Value, sldEixoY.Value, MapAnimationKind.Bow);
                await mapFindBus.TrySetViewAsync(centroAtualDoMapa, sldZoom.Value, sldEixoX.Value, sldEixoY.Value, MapAnimationKind.Bow);
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
            //posicao = new Geopoint(new BasicGeoposition() { Latitude = gp.Coordinate.Point.Position.Latitude, Longitude = gp.Coordinate.Point.Position.Longitude });
            posicao = new Geopoint(new BasicGeoposition() { Latitude = -15.793257, Longitude = -47.883268 });
     
            //mapFindBus.Center = posicao;

            sldZoom.Value = SLIDER_ZOOM_INICIAL;

            AddMapIcon(posicao);
            MostrarPosicao(posicao);

            sleep(DELAY_SEG);

            iniciarContador();
        }

        private void mapFindBus_CenterChanged(MapControl sender, object args)
        {
            centroAtualDoMapa = mapFindBus.Center;
        }
    }
}
