
using Fragrance_flow_DL_VERSION_.models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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
        private async void BtnBanUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var seletedUser = ListBox2.SelectedItem as Users;

                if (seletedUser == null) MessageBox.Show("Please selected a user to ban");




                using (HttpClient client = new HttpClient())
                {

                    var userdata = new
                    {
                        seletedUser.id
                    };

                    var json = JsonConvert.SerializeObject(userdata);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PatchAsync($"https://localhost:7014/api/Fragrance_Flow/Users/Admin/Ban", content);

                    if (response.IsSuccessStatusCode) MessageBox.Show($"Successfully banned user : {seletedUser.username}");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($" An error occured banning user : {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetUsers();
        }

        private async void ButtonUnban_Click(object sender, RoutedEventArgs e)
        {




            var selectedUser = ListBox2.SelectedItem as Users;

            if (selectedUser == null) MessageBox.Show("Please selected a user to ban");

            var msg = MessageBox.Show($" Do you want to ban user : {selectedUser.username}", "Select", MessageBoxButton.YesNoCancel);

            if (msg == MessageBoxResult.No) return;
            if (msg == MessageBoxResult.Cancel) return;



            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var userdata = new
                    {
                        selectedUser.id
                    };
                    
                    var json = JsonConvert.SerializeObject(userdata);
                    var content = new StringContent (json, Encoding.UTF8, "application/json");

                    var response = await client.PatchAsync("https://localhost:7014/api/Fragrance_Flow/Users/Admin/Unban", content);
                    
                    if (response.StatusCode == System.Net.HttpStatusCode.OK) MessageBox.Show($"Successfully Unbanned user : {selectedUser.username}");
                    
                    return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" An error occured while unbanning user [{selectedUser.username}]: {ex.Message}","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
