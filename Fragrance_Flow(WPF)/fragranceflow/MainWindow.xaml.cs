using Fragrance_flow_DL_VERSION_.models;
using Fragrance_Flow_WPF_.fragranceflow;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
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

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();


                        var fragrances = JsonConvert.DeserializeObject<List<Fragrance>>(responseData);
                        Listbox1.ItemsSource = fragrances;





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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            Window addFragranceWindow = new AddFragranceWindow(_username);
            addFragranceWindow.Show();
        }
        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                using (HttpClient client = new HttpClient())
                {
                    var selectedFragrance = Listbox1.SelectedItem as Fragrance;


                    if (selectedFragrance == null)
                    {
                        MessageBox.Show(selectedFragrance.GetType().FullName);
                        return;
                    }





                    var idToDelete = selectedFragrance.id;



                    var response = await client.DeleteAsync($"https://localhost:7014/api/Fragrance_Flow/Fragrances/delete?username={_username}&id={idToDelete}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Fragrance remove successful", "Removal successful", MessageBoxButton.OK);
                    }
                    return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" An error occured : Failed to delete {ex.Message}");
            }
        }
    }

}