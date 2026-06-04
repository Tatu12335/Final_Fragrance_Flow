using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FRONTEND
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _token;
        public MainWindow(string token)
        {
            _token = token;
            InitializeComponent();
        }
        public string GetFragrances()
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                    return client.GetStringAsync("https://localhost:7014/api/Fragrance_Flow/Get-All").Result;
                }
            }
            catch(Exception ex)
            {
                this.Close();
                return $"An error occurred while trying to retrieve fragrances: {ex.Message}";
            }
        }
    }
}