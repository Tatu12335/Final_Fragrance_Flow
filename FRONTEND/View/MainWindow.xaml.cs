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
namespace FRONTEND.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _token;
        public MainWindow(string token)
        {
            InitializeComponent();
            DataContext = new View_Model.MainViewModel(token);
            _token = token;
           
        }

        private async Task Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync("https://localhost:7014/api/Fragrance_Flow/Get-All");
            }
        }
    }
}
