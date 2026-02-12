using Fragrance_flow_DL_VERSION_.models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace Fragrance_Flow_WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _username;
        public MainWindow(string username)
        {
            InitializeComponent();
            _username = username;

            try
            {
                GetFragrances();
                label1.Content = $" Welcome, {_username}!";
            }
            catch (Exception ex)
            {
                throw new Exception(" An error occured while loading the main window : " + ex.Message);
            }

        }
        public async void GetFragrances()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var userData = new
                    {
                        username = _username,
                        
                    };


                    var json = JsonConvert.SerializeObject(userData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("https://localhost:7014/api/Fragrance_Flow/Fragrances", content);
                    Listbox1.Items.Clear();
                    if (response.IsSuccessStatusCode)
                    {
                       foreach(var item in await response.Content.ReadFromJsonAsync<Fragrance[]>())
                        {
                            Listbox1.Items.Add($" {item.name} | {item.brand}");
                        }



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