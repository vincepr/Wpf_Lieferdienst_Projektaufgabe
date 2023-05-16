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
    /// Interaktionslogik für FensterBestellen.xaml
    /// </summary>
    public partial class FensterBestellen : Window
    {
        Food curWindowChoice;
        public FensterBestellen(Food choice)
        {
            InitializeComponent();
            // bind this Window to choice
            this.DataContext = choice;
            this.curWindowChoice = choice;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            closeWindow(false);
        }

        private async void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // parse the count
            bool isValid = uint.TryParse(txtAnzahl.Text, out uint anzahl);
            if (!isValid){
                closeWindow(false);
                return;
            }
            // we create a tempory anonimous object
            var data = new {curWindowChoice.eid, anzahl};
            // make it a json string
            string json = JsonConvert.SerializeObject(data);
            // build a request:
            HttpContent content = new StringContent(json);
            // make a Post Request to our pho server:
            HttpClient client = new HttpClient();
            await client.PostAsync("http://localhost/ProjectLieferdienst/bestellen.php", content);

            MessageBox.Show(json);
            closeWindow(true);
        }

        private void closeWindow(bool result)
        {
            // we set the result-status we can check for in our main-window.
            this.DialogResult = result;
            // we manually close the window
            this.Close();
        }
    }
}
