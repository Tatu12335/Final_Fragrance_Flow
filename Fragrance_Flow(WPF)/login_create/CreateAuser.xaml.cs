using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Fragrance_Flow_WPF_
{
    /// <summary>
    /// Interaction logic for CreateAuser.xaml
    /// </summary>
    public partial class CreateAuser : Window
    {

        public CreateAuser()
        {
            InitializeComponent();


        }

        private async void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordBox.Password) || string.IsNullOrEmpty(ConfirmPasswordBox.Password) || string.IsNullOrEmpty(EmailBox.Text))
            {
                MessageBox.Show(" Please fill in all fields.", " Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var userData = new
                    {
                        username = UsernameTextBox.Text,
                        email = EmailBox.Text,
                        password = PasswordBox.Password,
                    };


                    var json = JsonConvert.SerializeObject(userData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("https://localhost:7014/api/Fragrance_Flow/Users/Create", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(" Account created successfully! You can now log in.", " Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                        Loginwindow loginwindow = new Loginwindow();
                        loginwindow.Show();
                    }
                    else
                    {
                        MessageBox.Show($" An error occured : {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(" An error occurred while creating the account: " + ex.Message, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
