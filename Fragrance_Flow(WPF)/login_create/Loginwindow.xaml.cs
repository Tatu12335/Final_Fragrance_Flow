using Fragrance_flow_DL_VERSION_.Application.interfaces;
using Fragrance_flow_DL_VERSION_.Domain.Entities;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Fragrance_Flow_WPF_
{
    /// <summary>
    /// Interaction logic for Loginwindow.xaml
    /// </summary>


    public partial class Loginwindow : MetroWindow
    {
        
        
        public Loginwindow()
        {

            InitializeComponent();
            


        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = UsernameTextBox.Text;
                var password = PasswordBox.Password;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show(" Please enter both username and password.", " Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    await Task.CompletedTask;
                }

                using (HttpClient client = new HttpClient())
                {

                    var userData = new
                    {
                        username = UsernameTextBox.Text,
                        password = PasswordBox.Password,
                    };


                    var json = JsonConvert.SerializeObject(userData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                   

                    var response = await client.PostAsync("https://localhost:7014/api/Fragrance_Flow/Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                        if (string.IsNullOrEmpty(result.ToString())) return;

                        


                        MainWindow mainWindow = new MainWindow(username,result);

                        

                        
                        mainWindow.Show();

                        this.Close();


                    }
                    else
                    {
                        MessageBox.Show($" An error occured : {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Content = ex.Message;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e) // This event is Forgot password, i will implement it later, maybe with a security question or something, but for now, it will just be a placeholder.
        {

        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e) // This event is for the register hyperlink
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            this.Hide();
            CreateAuser createAuserWindow = new CreateAuser();
            createAuserWindow.Show();

        }
    }
    
}



