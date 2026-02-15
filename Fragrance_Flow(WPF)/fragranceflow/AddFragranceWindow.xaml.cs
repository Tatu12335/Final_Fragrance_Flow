using Fragrance_flow_DL_VERSION_.models;
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

namespace Fragrance_Flow_WPF_.fragranceflow
{
    /// <summary>
    /// Interaction logic for AddFragranceWindow.xaml
    /// </summary>
    public partial class AddFragranceWindow : Window
    {
        private string _username;
        public AddFragranceWindow(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var userData = new
                    {
                        username = _username,
                        name = txtFragranceName,
                        brand = txtBrand,
                        notes = txtNotes,
                        occasion = txtOccasion,
                        weather = txtWeather,



                    };


                    var json = JsonConvert.SerializeObject(userData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync($"https://localhost:7014/api/Fragrance_Flow/Fragrances/Add/{_username}", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                       
                        MessageBox.Show(content.ToString(), "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    else
                    {
                        MessageBox.Show($" An error occured 2 : {response.ReasonPhrase}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(" An error occured while fetching fragrances : " + ex.Message);
            }
        }
    }
}
