using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AcheoOnibus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            List<Itinerary> iti = getItinerary();

            foreach (Itinerary itinerary in iti)
            {
                cmbSelection.Items.Add(itinerary.routeNumber);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private List<Itinerary> getItinerary()
        {
            HttpClient cliente = new HttpClient();

            HttpResponseMessage resposta = cliente.GetAsync("http://sdo-server.herokuapp.com/find/bus/itinerary").Result;

            HttpContent stream = resposta.Content;

            var resultadoLista = stream.ReadAsStringAsync();

            List<Itinerary> listLocation = JsonConvert.DeserializeObject<List<Itinerary>>(resultadoLista.Result);

            return listLocation;
        }

        private void btnSend_Click_1(object sender, RoutedEventArgs e)
        {
            HttpClient cliente = new HttpClient();
            HttpResponseMessage resposta = cliente.GetAsync("http://sdo-server.herokuapp.com/find/bus/position/byLineItinerary/" + cmbSelection.SelectedItem.ToString()).Result;
            HttpContent stream = resposta.Content;
            var resultadoLista = stream.ReadAsStringAsync();
            List<Location> listLocation = JsonConvert.DeserializeObject<List<Location>>(resultadoLista.Result);

            Frame.Navigate(typeof(MapPage), listLocation);
        }
    }
}
