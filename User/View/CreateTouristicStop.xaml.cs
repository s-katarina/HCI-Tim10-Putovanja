using HCI_Tim10_Putovanja.Core;
using Microsoft.Maps.MapControl.WPF;
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
using static HCI_Tim10_Putovanja.User.View.UpdateTouristicStop;

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for CreateTouristicStop.xaml
    /// </summary>
    public partial class CreateTouristicStop : Page
    {
        private static readonly string coordinateStringOrigin = "43.620574,22.34942";

        private TSDataContext tsDataContext;
        public CreateTouristicStop()
        {
            InitializeComponent();
            tsDataContext = new TSDataContext(null, null,coordinateStringOrigin);
            DataContext = tsDataContext;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (tsDataContext.TSName == null || tsDataContext.LocationAddress == null)
            {
                MessageBox.Show("Molimo vas popunite sva polja.", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string[] latlong = tsDataContext.CoordinatesString.Split(",", 2);
            double lat = 0;
            Double.TryParse(latlong[0], out lat);
            double lon = 0;
            Double.TryParse(latlong[1], out lon);
            AllTouristicStops.TouristicStops.Add(new TuristicStops(tsDataContext.TSName, new Location(lat, lon, tsDataContext.LocationAddress)));
            MessageBox.Show("Uspesno kreirano!", "Uspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Information);
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
            // Determine whether startPin or endPin has been moved and update with new location
            mapPin.Location = SelectedPushpin.Location;
            MapService.GetAddressForCreateTouristicStop(mapPin.Location.Latitude, mapPin.Location.Longitude, (TSDataContext) DataContext, this);
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
