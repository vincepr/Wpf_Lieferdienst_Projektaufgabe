﻿using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Lieferdienst
{
    public partial class MainWindow : Window {

        // Our data:
        List<Food> foods = new List<Food>();

        // Main Method:
        public MainWindow() {
            InitializeComponent();
        }

        // Task are the async "Threads" of C# 
        private async Task RequestDataFromPhp() {
            // make a HTTP request
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost/ProjectLieferdienst/essen.php");
            if (response.IsSuccessStatusCode) {
                string json = await response.Content.ReadAsStringAsync();
                foods = JsonConvert.DeserializeObject<List<Food>>(json);
                btnAnzeige.IsEnabled = true;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            // blocks till we have data from our Request:
            await RequestDataFromPhp();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // ItemSource={Binding} in xaml and foods-List get connected:
            listView.DataContext = foods;
        }
    }

    // the wrapper for our Data coming as JSON form the DB/PHP pipeline
    public class Food {
        public int eid { get; set; }
        public string bezeichnung { get; set; }
        public double preis { get; set; }

        public override string ToString() {
            string bez = bezeichnung.ToString();
            if (bez.Length > 7) return $"{eid} \t| {bezeichnung}\t| {preis}";
            return $"{eid} \t| {bezeichnung} \t\t| {preis}";
        }
    }
}