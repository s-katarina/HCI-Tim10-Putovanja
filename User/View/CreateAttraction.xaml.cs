﻿using HCI_Tim10_Putovanja.Core;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for CreateAttraction.xaml
    /// </summary>
    public partial class CreateAttraction : Page
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

        private ICommand saveCommand;
        public ICommand SaveCommand {
            get
            {
                return saveCommand
                    ?? (saveCommand = new SaveCommand(() =>
                    {
                        Save();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (name == null || addr == null)
            {
                MessageBox.Show("Molimo vas popunite polja Naziv i Lokacija.", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string[] latlong = CoordinatesString.Split(",", 2);
            double lat = 0;
            Double.TryParse(latlong[0], out lat);
            double lon = 0;
            Double.TryParse(latlong[1], out lon);
            AllAttractions.Attractions.Add(new Atraction(name, new Location(lat, lon, addr), desc, images));
            MessageBox.Show("Uspesno kreirana atrakcija!", "Uspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Information);
            AllAttractions page = new AllAttractions(Database.Attractions);
            this.NavigationService.Navigate(page);

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
            MapService.GetAddressForCreateAttraction(mapPin.Location.Latitude, mapPin.Location.Longitude, this);
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

public class SaveCommand : ICommand
{
    private readonly Action _action;

    public SaveCommand(Action action)
    {
        _action = action;
    }

    public void Execute(object parameter)
    {
        _action();
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public event EventHandler CanExecuteChanged;
}
