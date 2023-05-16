using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Lieferdienst
{
    public partial class MainWindow : Window
    {

        // Our data:
        List<Food> foods = new List<Food>();

        // Main Method:
        public MainWindow()
        {
            InitializeComponent();
        }

        // Task are the async "Threads" of C# 
        private async Task RequestDataFromPhp()
        {
            // make a HTTP request
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost/ProjectLieferdienst/essen.php");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                foods = JsonConvert.DeserializeObject<List<Food>>(json);
                btnAnzeige.IsEnabled = true;

                listView.DataContext = foods;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // await -> blocks till we have data from our Request:
            await RequestDataFromPhp();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // ItemSource={Binding} in xaml and foods-List get connected:
            listView.DataContext = foods;
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food choice = listView.SelectedValue as Food;

            // We create the new FensterBestellen
            FensterBestellen window = new FensterBestellen(choice);
            // ShowDialog() blocks the current window till it is closed. Show() does not.
            // we check for the result coming from the FensterBestellen.DialogResult
            bool? result = window.ShowDialog();
            if (result == true)
                MessageBox.Show("Vielen Dank für ihre bestellung");
            else if (result == false)
                MessageBox.Show("Clicked Cancel");
            else
               MessageBox.Show("some other close (taskmanger/crash etc)");
        }
    }


    // the wrapper for our Data coming as JSON form the DB/PHP pipeline
    public class Food {
        public int eid { get; set; }
        public string bezeichnung { get; set; }
        public double preis { get; set; }
        public string info { get; set; }
        public string bild { get; set; }
        public string GetPreis => preis.ToString()+" €";

        public override string ToString() {
            string bez = bezeichnung.ToString();
            if (bez.Length > 7) return $"{eid} \t| {bezeichnung}\t| {preis} \t| {info}";
            return $"{eid} \t| {bezeichnung} \t\t| {preis} \t| {info}";
        }
    }
}
