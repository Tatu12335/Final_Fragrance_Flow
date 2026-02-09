using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Fragrance_Flow_WPF_
{
    /// <summary>
    /// Interaction logic for Loginwindow.xaml
    /// </summary>


    public partial class Loginwindow : Window
    {

        // https://localhost:7014/api/Fragrance_Flow/Users?username
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

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://localhost:7014/api/Fragrance_Flow/Users?username={username}");
                    if (response != null)
                    {
                        var userInfo = await response.Content.ReadFromJsonAsync<Users>();

                        if (userInfo == null)
                        {

                            ErrorMessage.Content = new Exception(" User not found. Please check your username and try again.");

                        }
                        else
                        {
                            Passwordhasher hasher = new Passwordhasher();
                            if (hasher.VerifyPassword(password, userInfo.PasswordHash, Convert.FromHexString(userInfo.salt)))
                            {
                                MainWindow mainWindow = new MainWindow(username);
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                ErrorMessage.Content = new Exception(" Incorrect password. Please try again.");
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage.Content = new Exception(" Error connecting to the server.");
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Content = ex.Message;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e) // Forgot password button
        {
            MessageBox.Show(" Registration functionality is not implemented yet. Please contact support for assistance.", " Registration", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Register button
        {
            MessageBox.Show(" Registration functionality is not implemented yet. Please contact support for assistance.", " Registration", MessageBoxButton.OK, MessageBoxImage.Information);
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



