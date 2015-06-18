using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public Geopoint Posicao { get; set; }

        string itinerario;
        string sentidoViagem;
        bool mostraUsuario = true;

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
                sentidoViagem = listaDeParametros[1].ToString();
            }

            RecuperaPosicaoAtual();

            /*
            foreach (Location location in listLocation)
            {
                showPosition(location);
            }
            //*/
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
            Posicao = new Geopoint(new BasicGeoposition() { Latitude = gp.Coordinate.Point.Position.Latitude, Longitude = gp.Coordinate.Point.Position.Longitude });
            MostrarPosicao(Posicao);
        }
    }
}
