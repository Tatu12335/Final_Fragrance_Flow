using Azure.Core;
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Fragrance_Flow_WPF_
{
    /// <summary>
    /// Interaction logic for Loginwindow.xaml
    /// </summary>


    public partial class Loginwindow : Window
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
                        
                        
                        MainWindow mainWindow = new MainWindow(username);
                        mainWindow.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show($" An error occured : {response.StatusCode} {response.ReasonPhrase}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    public class Passwordhasher : IPasswordhasher
    {

        private const int saltSize = 64;
        private const int iterations = 350000;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                saltSize
            );
            return Convert.ToHexString(hash);
        }
        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    hashAlgorithm,
                    saltSize);
            // Used fixed time comparison to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}



