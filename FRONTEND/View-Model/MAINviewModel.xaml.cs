using Fragrance_flow_DL_VERSION_.Domain.Entities;
using FRONTEND.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace FRONTEND.View_Model
{
    /// <summary>
    /// Interaction logic for MAINviewModel.xaml
    /// </summary>
    
        public class MainViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public ObservableCollection<FRAGRANCE> Fragrances { get; set; }
            private FRAGRANCE _selectedFragrance;
            private LoginResponse _token;
            public FRAGRANCE SelectedFragrance
            {
                get { return _selectedFragrance; }
                set
                {
                    if (_selectedFragrance != value)
                    {
                        _selectedFragrance = value;
                        OnPropertyChanged(nameof(SelectedFragrance));
                    }
                }
            }
            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        public async Task LoadFragrances()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token.token);
                var fragrances = await client.GetFromJsonAsync<List<FRAGRANCE>>("https://localhost:7014/api/Fragrance_Flow/Get-All");
                
                Fragrances.Clear();

                foreach (var fragrance in fragrances)
                {
                    Fragrances.Add(fragrance);
                }
            }
        }
            public MainViewModel(LoginResponse token)
            {
                _token = token;
                //Fragrances = new ObservableCollection<FRAGRANCE>();
                _ = LoadFragrances();

            }
            
        }
    
}
