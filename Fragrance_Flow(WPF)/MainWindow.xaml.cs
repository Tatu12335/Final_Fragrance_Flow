using Fragrance_flow_DL_VERSION_.models;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Json;
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
                using (var client = new HttpClient())
                {
                    
                    var response = await client.GetAsync($"https://localhost:7014/api/Fragrance_Flow/Fragrances?username={_username}");

                    
                    if (response != null)
                    {

                        var userInfo = await response.Content.ReadFromJsonAsync<Fragrance>();

                        if (userInfo == null) throw new Exception("Failed to deserialize the response into a Fragrance object.");




                        Listbox1.ItemsSource = $"{userInfo.Brand},{userInfo.Brand}";
                       
                        


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