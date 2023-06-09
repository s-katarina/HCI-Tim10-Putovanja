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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for CreateAttraction.xaml
    /// </summary>
    public partial class CreateAttraction : Page
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

        private List<string> images = new List<string>();
        public List<string> Images
        {
            get => images;
            set
            {
                if (value != images) images = value;
                OnPropertyChanged("Images");
            }
        }

        public CreateAttraction()
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
                MessageBox.Show("Molimo vas popunite polja Naziv i Lokacija.", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AllAttractions.Attractions.Add(new Atraction(name, new Location(0, 0, addr), desc, images));
            MessageBox.Show("Uspesno kreirana atrakcija!", "Uspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddImages(object sender, RoutedEventArgs e)
        {

        }

    }
}
