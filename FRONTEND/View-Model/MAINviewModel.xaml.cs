using Fragrance_flow_DL_VERSION_.Domain.Entities;
using FRONTEND.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            public MainViewModel(LoginResponse token)
            {
                _token = token;
                Fragrances = new ObservableCollection<FRAGRANCE>();

            }
            // This is where you would put properties and commands for the main view
        }
    
}
