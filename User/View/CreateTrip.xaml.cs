using HCI_Tim10_Putovanja.Core;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections;
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
    /// Interaction logic for CreateTrip.xaml
    /// </summary>
    public partial class CreateTrip : Page
    {

        private Trip trip;
        private ArrayList cbAttractionsItems;
        private ArrayList cbTouristicStopsItems;
        private ArrayList TouristicStps;
        private TripDataContext tdt;
        public ArrayList coordinatesString;

        private static readonly string coordinateStringOrigin = "43.620574,22.34942";
        private static readonly string coordinateStringDest = "42.620574,22.34942";


        public CreateTrip()
        {
            DataContext = this;
            trip = new Trip();
            trip.StartTime = DateTime.Now;
            trip.EndTime = DateTime.Now;
            tdt = new TripDataContext(trip, "", "");
            cbAttractionsItems = LoadCbAttractionsData();
            cbTouristicStopsItems = LoadCbTouristicStopsData();
            this.coordinatesString = new ArrayList();
            this.coordinatesString.Add(coordinateStringOrigin);
            this.coordinatesString.Add(coordinateStringDest);
            InitializeComponent();
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

        public ArrayList CoordinatesString
        {
            get => coordinatesString;
            set
            {
                if (value != coordinatesString)
                {
                    coordinatesString = value;
                    OnPropertyChanged("CoordinatesString");
                }
            }
        }

        public TripDataContext Tdt
        {
            get => tdt;
            set
            {
                if (value != tdt)
                {
                    tdt = value;
                    OnPropertyChanged("Tdt");
                }
            }
        }

        public Trip Trip
        {
            get => trip;
            set
            {
                if (value != trip)
                {
                    trip = value;
                    OnPropertyChanged("Trip");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnLoadAttr(object sender, RoutedEventArgs e)
        {
            cbOtherAttractions.ItemsSource = cbAttractionsItems;
        }

        private ArrayList LoadCbAttractionsData()
        {
            ArrayList vals = new ArrayList();
            vals.AddRange(Database.Attractions);
            return vals;
        }

        private void AddAttraction(object sender, RoutedEventArgs e)
        {
            if ((Atraction)cbOtherAttractions.SelectedItem != null)
            {
                trip.Atractions.Add((Atraction)cbOtherAttractions.SelectedItem);
                cbAttractionsItems.Remove((Atraction)cbOtherAttractions.SelectedItem);
                cbOtherAttractions.Items.Refresh();
                lbAttractions.Items.Refresh();
            }
        }

        private void RemoveAttraction(object sender, RoutedEventArgs e)
        {
            if ((Atraction)lbAttractions.SelectedItem != null)
            {
                trip.Atractions.Remove((Atraction)lbAttractions.SelectedItem);
                cbAttractionsItems.Add((Atraction)lbAttractions.SelectedItem);
                cbOtherAttractions.Items.Refresh();
                lbAttractions.Items.Refresh();
            }
        }



        private void OnLoadTouristicStop(object sender, RoutedEventArgs e)
        {
            cbOtherTouristicStops.ItemsSource = cbTouristicStopsItems;
        }

        private ArrayList LoadCbTouristicStopsData()
        {
            ArrayList vals = new ArrayList();
            vals.AddRange(Database.TouristicStops);
            return vals;
        }

        private void AddTouristicStop(object sender, RoutedEventArgs e)
        {
            if ((TuristicStops)cbOtherTouristicStops.SelectedItem != null)
            {
                trip.TouristicStops.Add((TuristicStops)cbOtherTouristicStops.SelectedItem);
                cbTouristicStopsItems.Remove((TuristicStops)cbOtherTouristicStops.SelectedItem);
                cbOtherTouristicStops.Items.Refresh();
                lbTouristicStops.Items.Refresh();
            }
        }

        private void RemoveTouristicStop(object sender, RoutedEventArgs e)
        {
            if ((TuristicStops)lbTouristicStops.SelectedItem != null)
            {
                trip.TouristicStops.Remove((TuristicStops)lbTouristicStops.SelectedItem);
                cbTouristicStopsItems.Add((TuristicStops)lbTouristicStops.SelectedItem);
                cbOtherTouristicStops.Items.Refresh();
                lbTouristicStops.Items.Refresh();
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtStartTime.Text == "" || txtEndTime.Text == ""
                   || txtStartLocation.Text == "" || txtEndLocation.Text == "" || txtPrice.Text == ""
                   || txtDesc.Text == "")
            {
                MessageBox.Show("Molimo vas popunite sva polja", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (trip.Price < 1000 || trip.Price > 100000)
            {
                MessageBox.Show("Cena se krece u opsegu od 1000 do 100 000. Molimo vas popunite polje ponovo sa ispravnom vrednoscu.", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                trip.StartTime = DateTime.Parse(txtStartTime.Text);
                trip.EndTime = DateTime.Parse(txtEndTime.Text);
            } catch (Exception)
            {
                MessageBox.Show("Polje za vreme je formata dd.MM.yyyy. HH:mm. Molimo vas popunite polje ponovo sa ispravnom vrednoscu.", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                trip.Price = double.Parse(txtPrice.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Polje Cena se unosi u ciframa.", "Neuspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            trip.Name = txtName.Text;
            trip.StartLocation = tdt.trip.StartLocation;
            trip.EndLocation = tdt.trip.EndLocation;
            trip.Description = txtDesc.Text;
            Database.Trips.Add(trip);
            MessageBox.Show("Uspesno kreirano putovanje!", "Uspesno kreiranje", MessageBoxButton.OK, MessageBoxImage.Information);
            AllTrips page = new AllTrips(Database.Trips);
            this.NavigationService.Navigate(page);
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
            if (startPin.Location.Latitude == SelectedPushpin.Location.Latitude && startPin.Location.Longitude == SelectedPushpin.Location.Longitude)
            {
                startPin.Location = SelectedPushpin.Location;
                MapService.GetAddressForCreateTrip(startPin.Location.Latitude, startPin.Location.Longitude, tdt, true, this);
            }
            else
            {
                endPin.Location = SelectedPushpin.Location;
                MapService.GetAddressForCreateTrip(endPin.Location.Latitude, endPin.Location.Longitude, tdt, false, this);
            }
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


    }
}
