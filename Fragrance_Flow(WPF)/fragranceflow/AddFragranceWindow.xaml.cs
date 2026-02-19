using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

                    var fragranceData = new
                    {
                        name = txtFragranceName.Text,
                        brand = txtBrand.Text,
                        notes = txtNotes.Text,
                        category = txtCategory.Text,
                        weather = txtWeather.Text,
                        occasion = txtOccasion.Text,
                    };


                    var json = JsonConvert.SerializeObject(fragranceData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"https://localhost:7014/api/Fragrance_Flow/Fragrances/Add?username={_username}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(json, "Sent data", MessageBoxButton.OK);
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($" An error occured 2 : {response.StatusCode} - {error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" An error occured :{ex.Message}", "Fatal error");
            }
        }

        private void txtFragranceName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
