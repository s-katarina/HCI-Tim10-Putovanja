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
    /// Interaction logic for UpdateTouristicStop.xaml
    /// </summary>
    public partial class UpdateTouristicStop : Page
    {
        public UpdateTouristicStop()
        {
            InitializeComponent();
        }

        public UpdateTouristicStop(TuristicStops ts)
        {
            InitializeComponent();
            DataContext = this;
            name = ts.Name;
            addr = ts.Location.Address;
            touristic_stop = ts;
        }

        private TuristicStops touristic_stop;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da izmenite? Kliknite OK za potvrdu.", "Potvrda izmena", System.Windows.MessageBoxButton.OK);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                if (name == null || addr == null)
                {
                    MessageBox.Show("Molimo vas popunite sva polja.", "Neuspesna izmena", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                touristic_stop.Name = name;
                touristic_stop.Location.Address = addr;
                MessageBox.Show("Uspesno izmenjeno!", "Uspesna izmena", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da obrisete? Kliknite OK za potvrdu.", "Potvrda brisanja", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                AllTouristicStops.TouristicStops.Remove(touristic_stop);
                MessageBox.Show("Uspesno obrisano!", "Uspesno brisanje", MessageBoxButton.OK, MessageBoxImage.Information);
                AllTouristicStops page = new AllTouristicStops(AllTouristicStops.TouristicStops);
                this.NavigationService.Navigate(page);
            }
        }

    }
}
