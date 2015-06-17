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

            List<Itinerario> iti = getItinerario();

            if (iti != null && iti.Count > 0)
            {
                foreach (Itinerario itinerary in iti)
                {
                    cmbSelection.Items.Add(itinerary.numero);
                }
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

        private List<Itinerario> getItinerario()
        {
            try
            {
                HttpClient cliente = new HttpClient();
                //HttpResponseMessage resposta = cliente.GetAsync("http://sdo-server.herokuapp.com/find/bus/itinerary").Result;
                HttpResponseMessage resposta = cliente.GetAsync("http://localhost:1916/api/Itinerarios").Result;
                HttpContent stream = resposta.Content;
                var resultadoLista = stream.ReadAsStringAsync();
                List<Itinerario> listLocation = JsonConvert.DeserializeObject<List<Itinerario>>(resultadoLista.Result);
                return listLocation;
            }
            catch (Exception)
            {
                ExibirMensagem("Erro", "Erro ao buscar itinerários.");
                throw;
            }
        }

        private List<Viagem> getViagens()
        {
            try
            {
                HttpClient cliente = new HttpClient();
                HttpResponseMessage resposta = cliente.GetAsync("http://localhost:1916/api/Viagens").Result;
                HttpContent stream = resposta.Content;
                var resultadoLista = stream.ReadAsStringAsync();
                List<Viagem> listViagens = JsonConvert.DeserializeObject<List<Viagem>>(resultadoLista.Result);
                return listViagens;
            }
            catch (Exception)
            {
                ExibirMensagem("Erro", "Erro ao buscar viagens.");
                throw;
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

        private void btnSend_Click_1(object sender, RoutedEventArgs e)
        {
            //HttpClient cliente = new HttpClient();
            //HttpResponseMessage resposta = cliente.GetAsync("http://sdo-server.herokuapp.com/find/bus/position/byLineItinerary/" + cmbSelection.SelectedItem.ToString()).Result;
            //HttpContent stream = resposta.Content;
            //var resultadoLista = stream.ReadAsStringAsync();
            //List<Location> listLocation = JsonConvert.DeserializeObject<List<Location>>(resultadoLista.Result);

            //Frame.Navigate(typeof(MapPage), listLocation);
        }

        private void cmbSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Viagem> viagens = getViagens();

            if (viagens != null && viagens.Count > 0)
            {
                foreach (Viagem viagem in viagens)
                {
                    cmbSentidoViagem.Items.Add(viagem.destino + "/" + viagem.origem);
                }
            }

            cmbSentidoViagem.IsEnabled = true;
        }
    }
}
