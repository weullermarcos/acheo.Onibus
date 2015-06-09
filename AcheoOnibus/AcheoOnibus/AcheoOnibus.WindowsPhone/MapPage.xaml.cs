using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
            List<Location> listLocation = e.Parameter as List<Location>;

            foreach (Location location in listLocation)
            {
                showPosition(location);
            }
        }

        private void AddMapIcon(Geopoint point)
        {
            MapIcon icon = new MapIcon();
            icon.Location = point;
            icon.NormalizedAnchorPoint = new Point(0.0, 0.0);
            icon.Title = "0.xxx";
            icon.Image =
                        RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/bus.png"));
            mapFindBus.MapElements.Add(icon);
            //mapFindBus.MapElements.Remove(icon);
        }

        private async void showPosition(Location location)
        {
            //position = new Geopoint(new BasicGeoposition() { Latitude = -15.823127, Longitude = -47.9208744 });
            position = new Geopoint(new BasicGeoposition() { Latitude = location.latitude, Longitude = location.longitude });
            try
            {
                 mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.AerialWithRoads;
                //mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.Road;

                AddMapIcon(position);

                await mapFindBus.TrySetViewAsync(position, 18.0, 0, 0, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
