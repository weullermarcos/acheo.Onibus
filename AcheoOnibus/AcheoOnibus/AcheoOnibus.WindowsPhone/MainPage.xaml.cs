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
        List<Itinerario> listaDeItinerarios = new List<Itinerario>();
        List<Viagem> listaDeViagens = new List<Viagem>();
        int idItinerarioSelecionado = 0;
        
        Dictionary<string, int> dicionarioSentidoViagem = new Dictionary<string, int>();


        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            listaDeItinerarios = getItinerario();

            if (listaDeItinerarios != null && listaDeItinerarios.Count > 0)
            {
                foreach (Itinerario itinerary in listaDeItinerarios)
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
            string mensagem = e.Parameter as string;

            if (mensagem.Contains("Erro"))
            {
                exibirMensagem("", mensagem);
            }

        }

        private List<Itinerario> getItinerario()
        {
            try
            {
                HttpClient cliente = new HttpClient();
                HttpResponseMessage resposta = cliente.GetAsync("http://localhost:1916/api/Itinerarios").Result;
                HttpContent stream = resposta.Content;
                var resultadoLista = stream.ReadAsStringAsync();
                List<Itinerario> listLocation = JsonConvert.DeserializeObject<List<Itinerario>>(resultadoLista.Result);
                return listLocation;
            }
            catch (Exception)
            {
                exibirMensagem("Erro", "Erro ao buscar itinerários.");
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
                exibirMensagem("Erro", "Erro ao buscar viagens.");
                throw;
            }
        }

        private async void exibirMensagem(string titulo, string mensagem)
        {
            ContentDialog d = new ContentDialog();
            d.Title = titulo;
            d.Content = mensagem;
            d.PrimaryButtonText = "Ok";
            await d.ShowAsync();
        }

        private void cmbSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbSentidoViagem.Items.Clear();
            dicionarioSentidoViagem.Clear();
            btnSend.IsEnabled = false;

            listaDeViagens = getViagens();

            idItinerarioSelecionado = getIdItinerarioSelecionado();

            if (listaDeViagens != null && listaDeViagens.Count > 0)
            {
                foreach (Viagem viagem in listaDeViagens)
                {
                    if (idItinerarioSelecionado == viagem.idItinerarioFK)
                    {
                        string auxiliar = viagem.origem + "/" + viagem.destino;
                        cmbSentidoViagem.Items.Add(auxiliar);
                        dicionarioSentidoViagem.Add(auxiliar, viagem.sentidoViagem);
                    }
                }
            }

            cmbSentidoViagem.IsEnabled = true;
        }

        private int getIdItinerarioSelecionado() 
        {
            foreach (Itinerario itinerario in listaDeItinerarios)
            {
                if (itinerario.numero == cmbSelection.SelectedItem.ToString())
                {
                    return itinerario.idItinerario;
                }
            }

            return 0;
        }

        private void btnSend_Click_1(object sender, RoutedEventArgs e)
        {
            List<object> listaDeParametros = new List<object>();

            listaDeParametros.Add(cmbSelection.SelectedItem.ToString());

            foreach (KeyValuePair<string, int> viagem in dicionarioSentidoViagem)
            {
                if (viagem.Key.ToString() == cmbSentidoViagem.SelectedItem.ToString())
                {
                    listaDeParametros.Add(viagem.Value.ToString());
                }
                //MessageBox.Show(pair.Key.ToString() + "  -  " + pair.Value.ToString());
            }

            Frame.Navigate(typeof(MapPage), listaDeParametros);
        }

        private void cmbSentidoViagem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSend.IsEnabled = true;
        }
    }
}
