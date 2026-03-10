
using Fragrance_flow_DL_VERSION_.models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Fragrance_flow_DL_VERSION_.interfaces;
using System.Net.Http.Json;

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
            



            this.Loaded += async (s, e) =>
            {
                try
                {
                    await GetUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($" An error occured while loading users : {ex.Message}", " Error", MessageBoxButton.OK);
                    return;
                }
            };
        }
        public async Task GetUsers() 
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

                       
                       ListBox2.ItemsSource = new ObservableCollection<Users>(users);
                    }
                    
                     
                    else
                    {
                        MessageBox.Show($" An error occured : {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                
                var selectedUser = ListBox2.SelectedItem as Users;

                if (selectedUser == null)
                {
                    MessageBox.Show("Please selected a user to ban");
                    return;
                }


                using (HttpClient client = new HttpClient())
                {

                    var userdata = new
                    {
                        id = selectedUser.id
                    };
                    if (selectedUser.isBanned == 1)
                    {
                        MessageBox.Show("Cant ban user that is already banned", "Warning!", MessageBoxButton.OK);
                        return;
                    }

                    var msg = MessageBox.Show($" Are you sure you want to ban user : {selectedUser.username}","Confirm",MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                    if (msg == MessageBoxResult.Yes)
                    {

                        var json = JsonConvert.SerializeObject(userdata);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        
                        var response = await client.PatchAsync($"https://localhost:7014/api/Fragrance_Flow/Users/Admin/Ban",content);
                        
                        
                        
                        if (response.StatusCode == System.Net.HttpStatusCode.OK) MessageBox.Show($"Successfully banned user : {selectedUser.username}");
                        await GetUsers();
                    }
                    


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($" An error occured banning user : {ex.Message}");
            }
        }
        private async void ReFreshButton_Click(object sender, RoutedEventArgs e)
        {
            await GetUsers();
        
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GetUsers();    
            }
            catch(Exception ex)
            {
                MessageBox.Show($" An error occured while refreshing userlist : {ex.Message}","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private async void ButtonUnban_Click(object sender, RoutedEventArgs e)
        {

            var selectedUser = ListBox2.SelectedItem as Users;

            if (selectedUser == null)
            {
                MessageBox.Show("Please selected a user to ban");
                return;
            }
            try
            {
                var msg = MessageBox.Show($" Do you want to Unban user : {selectedUser.username}", "Select", MessageBoxButton.YesNoCancel);
                if (msg != MessageBoxResult.Yes) return;
            }
            catch(Exception ex)
            {
                MessageBox.Show($" An error occured : {ex.Message}");
            }
            

            try
            {
                using (HttpClient client = new HttpClient())
                {
                   

                   if(selectedUser.isBanned == 0)
                   {
                        MessageBox.Show("Cant unban user that is not banned" ,"Warning!" , MessageBoxButton.OK);
                        return;
                   }
                    var userdata = new
                    {
                        id = selectedUser.id,
                    };

                    var json = JsonConvert.SerializeObject(userdata);
                    var content = new StringContent (json, Encoding.UTF8,"application/json");

                    

                    var response = await client.PatchAsync("https://localhost:7014/api/Fragrance_Flow/Users/Admin/Unban", content );
                    
                    if (response.StatusCode == System.Net.HttpStatusCode.OK) MessageBox.Show($"Successfully Unbanned user : {selectedUser.username}");

                    

                    


                    await GetUsers();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" An error occured while unbanning user [{selectedUser.username}]: {ex.Message}","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
