
using Fragrance_flow_DL_VERSION_.models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Fragrance_Flow_WPF_.fragranceflow
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private string _username;

        public AdminPanel(string username)
        {
            InitializeComponent();
            _username = username;
            welcome.Content = $"Welcome {_username}";

            try
            {
                GetUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($" An error occured while loading users : {ex.Message}", " Error", MessageBoxButton.OK);
                return;
            }
        }
        public async void GetUsers()
        {

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    var response = await client.GetAsync("https://localhost:7014/api/Fragrance_Flow/Users/UserList");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();

                        var users = JsonConvert.DeserializeObject<IEnumerable<Users>>(responseData);

                        if (users == null) return;



                        ListBox2.ItemsSource = users;



                    }
                    else
                    {
                        MessageBox.Show($" An error occured : {response.ReasonPhrase}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(" An error occured while fetching users : " + ex.Message);
            }
        }
        private async Task BtnBanUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($" An error occured banning user : {ex.Message}");
            }
        }
    }
}
