using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf_Lieferdienst
{
    /// <summary>
    /// Interaktionslogik für FensterHistorie.xaml
    /// </summary>
    public partial class FensterHistorie : Window
    {
        List<Bestellung> curBestellungen;

        public FensterHistorie()
        {
            InitializeComponent();
            this.curBestellungen = new List<Bestellung>();
        }

        /*
         *      Logic for getting our Bestellungen -Data and drawing it.
         */

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshWindow(); // first time getting data and displaying it
        }

        private async Task RefreshWindow()
        {
            this.curBestellungen = await FetchHistorieFromPhp();
            this.DataContext = curBestellungen;
            setTotalSum(curBestellungen);
        }

        private async Task<List<Bestellung>> FetchHistorieFromPhp()
        {
            List<Bestellung> bestellungen = new List<Bestellung>();
            // make a HTTP request
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost/ProjectLieferdienst/historie.php");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                bestellungen = JsonConvert.DeserializeObject<List<Bestellung>>(json);
            }
            return bestellungen;
        }

        public void setTotalSum(List<Bestellung> listeBestellungen)
        {
            double sum = listeBestellungen.Aggregate(0.0, (double acc, Bestellung b) => acc + b.preis * b.anzahl);
            gesPreisLabel.Content = "Gesamtpreis: " + sum +" €";
        }

        /*
         *      Logic for Deleting Bestellungen
         */

        private async void DeleteBestellung_Double_Click(object sender, MouseButtonEventArgs e)
        {
            // get the currently selected (we just double clicked on it) Bbestellung:
            Bestellung curChoice = listView.SelectedValue as Bestellung;
            await PostRequest_Delete_Bestellung(curChoice);
            RefreshWindow();
        }

        private async Task PostRequest_Delete_Bestellung(Bestellung bestellung)
        {
            // we create a tempory anonimous object
            var data = new { bestellung.bid, bestellung.datum };
            // make it a json string
            string json = JsonConvert.SerializeObject(data);
            // build a request:
            HttpContent content = new StringContent(json);
            // make a Post Request to our pho server:
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://localhost/ProjectLieferdienst/delete.php", content);
            var txt = await response.Content.ReadAsStringAsync();
            MessageBox.Show("We affected that many rows (bigger than 0 is success):" + txt);
        }
    }
}
