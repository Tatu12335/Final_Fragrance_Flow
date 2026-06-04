using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net.Http;
namespace FRONTEND.SIGNIN
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        public async Task<string> SIGNIN(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }


            var userdata = new { username = username, password = password };

            var json = System.Text.Json.JsonSerializer.Serialize(userdata);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            try
            {
                using(var client = new HttpClient())
                {
                    var token = await client.PostAsync("https://localhost:7014/api/Fragrance_Flow/Login",content);
                    if (token == null)
                    {
                        MessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    return await token.Content.ReadAsStringAsync();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred while trying to log in: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string token = await SIGNIN(username, password);
            if (!string.IsNullOrEmpty(token))
            {
                MainWindow mainWindow = new MainWindow(token);         
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
