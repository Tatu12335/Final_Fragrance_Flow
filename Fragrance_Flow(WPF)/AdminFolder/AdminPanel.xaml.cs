using Fragrance_Flow_WPF_.AdminFolder;
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
        }

        private void BtnBanUser_Click(object sender, RoutedEventArgs e)
        {
            BanUserWindow banUser = new BanUserWindow();
            banUser.Show();
        }
    }
}
