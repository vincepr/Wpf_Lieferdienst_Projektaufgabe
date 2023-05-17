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
        private async Task FetchEssenListFromPhp()
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
            await FetchEssenListFromPhp();
        }

        // Anzeige 
        private void Button_Historie(object sender, RoutedEventArgs e)
        {
            FensterHistorie window = new FensterHistorie();
            bool? result = window.ShowDialog();
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
}
