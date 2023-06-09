using System;
using System.Collections.Generic;
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

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for UpdateAttraction.xaml
    /// </summary>
    public partial class UpdateAttraction : Page
    {
        private string name;
        public string AName
        {
            get => name;
            set
            {
                if (value != name) name = value;
                OnPropertyChanged("AName");
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
            }
        }

        private string desc;
        public string Description
        {
            get => desc;
            set
            {
                if (value != addr) desc = value;
                OnPropertyChanged("Description");
            }
        }

        private List<string> images;
        public List<string> Images
        {
            get => images;
            set
            {
                OnPropertyChanged("Images");
            }
        }
        public UpdateAttraction()
        {
            InitializeComponent();
        }

        public UpdateAttraction(Atraction attr)
        {
            InitializeComponent();
            this.name = attr.Name;
            this.addr = attr.Location.Address;
            this.desc = attr.Description;
            this.images = attr.Images;
            this.attraction = attr;
            DataContext = this;
        }

        private Atraction attraction;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da izmenite? Kliknite OK za potvrdu.", "Potvrda izmena", System.Windows.MessageBoxButton.OK);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                if (name == null || addr == null)
                {
                    MessageBox.Show("Molimo vas popunite polja Naziv i Lokacija.", "Neuspesna izmena", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                attraction.Name = name;
                attraction.Location.Address = addr;
                attraction.Description = desc;
                MessageBox.Show("Uspesno izmenjena atrakcija!", "Uspesna izmena", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddImages(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da obrisete atrakciju? Kliknite OK za potvrdu.", "Potvrda brisanja", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                AllAttractions.Attractions.Remove(attraction);
                MessageBox.Show("Uspesno obrisana atrakcija!", "Uspesno brisanje", MessageBoxButton.OK, MessageBoxImage.Information);
                AllAttractions page = new AllAttractions(AllAttractions.Attractions);
                this.NavigationService.Navigate(page);
            }
        }

    }
}
