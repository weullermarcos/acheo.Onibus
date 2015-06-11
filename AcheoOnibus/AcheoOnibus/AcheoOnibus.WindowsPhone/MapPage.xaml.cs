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

            /*
            foreach (Location location in listLocation)
            {
                showPosition(location);
            }
            //*/

            //*
            Location location1 = listLocation[0];
            Location location2 = listLocation[1];

            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = location1.latitude;
            startLocation.Longitude = location1.longitude;
            Geopoint startPoint = new Geopoint(startLocation);

            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = location2.latitude;
            endLocation.Longitude = location2.longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            ShowRouteOnMap(startPoint, endPoint);

            //*///Código de teste
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
                //mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.AerialWithRoads;
                mapFindBus.Style = Windows.UI.Xaml.Controls.Maps.MapStyle.Road;

                AddMapIcon(position);

                await mapFindBus.TrySetViewAsync(position, 18.0, 0, 0, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void ShowRouteOnMap(Geopoint startPoint, Geopoint endPoint)
        {

            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint);


            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapFindBus.Routes.Add(viewOfRoute);

                AddMapIcon(startPoint);
                AddMapIcon(endPoint);

                // Fit the MapControl to the route.
                await mapFindBus.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                //await mapFindBus.TrySetViewAsync(position, 18.0, 0, 0, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);
            }
        }

        private async void ShowRouteOnMap() // Do Ismael
        {
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = 40.7517;
            startLocation.Longitude = -073.9766;
            Geopoint startPoint = new Geopoint(startLocation);

            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = 40.7669;
            endLocation.Longitude = -073.9790;
            Geopoint endPoint = new Geopoint(endLocation);

            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint);


            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapFindBus.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapFindBus.TrySetViewBoundsAsync(
                    routeResult.Route.BoundingBox,
                    null,
                    Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }

    }
}
