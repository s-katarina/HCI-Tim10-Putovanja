using HCI_Tim10_Putovanja.Core;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        private static readonly string coordinateStringOrigin = "43.620574,22.34942";

        private string coordStr = coordinateStringOrigin;
        public string CoordinatesString
        {
            get => coordStr;
            set
            {
                if (value != coordStr) coordStr = value;
                OnPropertyChanged("CoordinatesString");
            }
        }
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
                if (value != images) images = value;
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
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da izmenite? Kliknite OK za potvrdu.", "Potvrda izmena", System.Windows.MessageBoxButton.OKCancel);
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
                attraction.Images = images;
                MessageBox.Show("Uspesno izmenjena atrakcija!", "Uspesna izmena", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddImages(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Izaberite fotografiju";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                BitmapImage bmpImg = new BitmapImage(new Uri(op.FileName));
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmpImg));
                string fileName = DateTime.Now.ToString();
                var invalids = System.IO.Path.GetInvalidFileNameChars();
                fileName = String.Join("_", fileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
                string dirPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "savedImg");
                Directory.CreateDirectory(dirPath);
                string imgPath = System.IO.Path.Combine(dirPath, fileName);
                using (Stream stm = File.Create(imgPath))
                {
                    encoder.Save(stm);
                    images.Add(imgPath);
                    lbImages.Items.Refresh();
                }
            }
        }

        private void RemoveImage(object sender, RoutedEventArgs e)
        {
            images.Remove((string)lbImages.SelectedItem);
            lbImages.Items.Refresh();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da obrisete atrakciju? Kliknite OK za potvrdu.", "Potvrda brisanja", System.Windows.MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                AllAttractions.Attractions.Remove(attraction);
                MessageBox.Show("Uspesno obrisana atrakcija!", "Uspesno brisanje", MessageBoxButton.OK, MessageBoxImage.Information);
                AllAttractions page = new AllAttractions(AllAttractions.Attractions);
                this.NavigationService.Navigate(page);
            }
        }

        private void MapWithPushpins_TouchDown(object sender, TouchEventArgs t)
        {
            Pushpin p = new Pushpin();
            p.Location = new Microsoft.Maps.MapControl.WPF.Location();
            myMap.Children.Add(new Pushpin());
        }

        Vector _mouseToMarker;
        private bool _dragPin;
        public Pushpin SelectedPushpin { get; set; }
        private Microsoft.Maps.MapControl.WPF.Location SelectedPushpinOriginLocation;

        void Pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            SelectedPushpin = sender as Pushpin;
            SelectedPushpinOriginLocation = SelectedPushpin.Location;
            _dragPin = true;
            _mouseToMarker = Point.Subtract(
              myMap.LocationToViewportPoint(SelectedPushpin.Location),
              e.GetPosition(myMap));
        }

        async void Pin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectedPushpin = sender as Pushpin;
            mapPin.Location = SelectedPushpin.Location;
            MapService.GetAddressForUpdateAttraction(mapPin.Location.Latitude, mapPin.Location.Longitude, this);
            e.Handled = true;
            SelectedPushpin = null;
            _dragPin = false;
        }

        private void MyMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (_dragPin && SelectedPushpin != null)
                {
                    SelectedPushpin.Location = myMap.ViewportPointToLocation(
                      Point.Add(e.GetPosition(myMap), _mouseToMarker));
                    e.Handled = true;
                }
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str);
            }
        }

    }
}
