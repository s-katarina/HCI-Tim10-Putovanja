using HCI_Tim10_Putovanja.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for CreateTouristicStop.xaml
    /// </summary>
    public partial class CreateTouristicStop : Page
    {

        private string name;
        public string TSName
        {
            get => name;
            set
            {
                if (value != name) name = value;
                OnPropertyChanged("TSName");
                Debug.WriteLine(value);

            }
        }

        private string addr;
        public string LocationAddress
        {
            get => addr;
            set
            {
                if (value != addr) addr = value;
                OnPropertyChanged("LocationAddress");
                Debug.WriteLine(value);
            }
        }

        public CreateTouristicStop()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (name == null || addr == null)
            {
                MessageBox.Show("Molimo vas popunite sva polja.", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AllTouristicStops.TouristicStops.Add(new TuristicStops(name, new Location(0, 0, addr)));
            MessageBox.Show("Uspesno kreirano!", "Uspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
