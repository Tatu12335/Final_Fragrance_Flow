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


                    if (response.IsSuccessStatusCode)
                    {

                        var userInfo = await response.Content.ReadFromJsonAsync<List<Fragrance>>();
                        if (userInfo != null)
                        {
                            Listbox1.ItemsSource = null;
                            Listbox1.Items.Clear();

                            foreach (var fragrance in userInfo)
                            {
                                string row = $"{fragrance.brand} - {fragrance.name}";
                                Listbox1.Items.Add(row);

                                //MessageBox.Show($"Fragrance Name: {fragrance.Name}\nBrand: {fragrance.Brand}\nNotes: {fragrance.notes}\nCategory: {fragrance.category}\nWeather: {fragrance.weather}\nOccasion: {fragrance.occasion}", "Fragrance Details", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            Listbox1.Items.Refresh();
                            //Listbox1.ItemsSource = $"{userInfo.Brand},{userInfo.Name}";

                        }
                        else
                        {
                            MessageBox.Show("No fragrance data found for the user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);




                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch fragrance data from the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

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