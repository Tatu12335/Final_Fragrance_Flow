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
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.classes.Services;
using Fragrance_flow_DL_VERSION_.classes;

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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
                var username = UsernameTextBox.Text;
                var password = PasswordBox.Password;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                


                
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
