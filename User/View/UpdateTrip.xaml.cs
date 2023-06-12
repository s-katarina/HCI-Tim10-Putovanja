using HCI_Tim10_Putovanja.Core;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for UpdateTrip.xaml
    /// </summary>
    public partial class UpdateTrip : Page
    {
        private Trip trip;
        private ArrayList cbAttractionsItems;
        private ArrayList cbTouristicStopsItems;
        private ArrayList TouristicStops;
        private TripDataContext tdt;

        public UpdateTrip()
        {
            InitializeComponent();
        }

        public UpdateTrip(Trip t)
        {
            trip = t;
            tdt = new TripDataContext(t, t.StartLocation.Address, t.EndLocation.Address);
            DataContext = tdt;
            TouristicStops = new ArrayList();
            cbAttractionsItems = LoadCbAttractionsData();
            cbTouristicStopsItems = LoadCbTouristicStopsData();
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
            foreach (Atraction attr in Database.Attractions)
            {
                if (!trip.Atractions.Contains(attr)) vals.Add(attr);
            }
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
            TouristicStops.AddRange(trip.TouristicStops);

            ArrayList vals = new ArrayList();
            foreach (TuristicStops ts in Database.TouristicStops)
            {
                if (!TouristicStops.Contains(ts)) vals.Add(ts);
            }
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

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da izmenite? Kliknite OK za potvrdu.", "Potvrda izmena", System.Windows.MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                if (txtName.Text == "" || txtStartTime.Text == "" || txtEndTime.Text == ""
                   || txtStartLocation.Text == "" || txtEndLocation.Text == "" || txtPrice.Text == ""
                   || txtDesc.Text == "")
                {
                    MessageBox.Show("Molimo vas popunite sva polja", "Neuspesna izmena", MessageBoxButton.OK, MessageBoxImage.Error);
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
                }
                catch (Exception)
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
                trip.StartLocation.Address = txtStartLocation.Text;
                trip.EndLocation.Address = txtEndLocation.Text;
                trip.Description = txtDesc.Text;
                MessageBox.Show("Uspesno izmenjeno putovanje!", "Uspesna izmena", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da obrisete putovanje? Kliknite OK za potvrdu.", "Potvrda brisanja", System.Windows.MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                if (!Database.CheckIfTripIsUsed(trip))
                {
                    Database.Trips.Remove(trip);
                    MessageBox.Show("Uspesno obrisano putovanje!", "Uspesno brisanje", MessageBoxButton.OK, MessageBoxImage.Information);
                    AllTrips page = new AllTrips(Database.Trips);
                    this.NavigationService.Navigate(page);
                } else
                {
                    MessageBox.Show("Putovanje je sadrzano u realizovanim (rezervisana i kupljena putovanja) i nije moguce obrisati ga.", "Neuspesno brisanje", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
            // Determine whether startPin or endPin has been moved and update with new location
            if (startPin.Location.Latitude == SelectedPushpin.Location.Latitude && startPin.Location.Longitude == SelectedPushpin.Location.Longitude)
            {
                startPin.Location = SelectedPushpin.Location;
                MapService.GetAddressForUpdateTrip(startPin.Location.Latitude, startPin.Location.Longitude, tdt, true, this);
            }
            else
            {
                endPin.Location = SelectedPushpin.Location;
                MapService.GetAddressForUpdateTrip(endPin.Location.Latitude, endPin.Location.Longitude, tdt, false, this);
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

    public class TripDataContext
    {
        public Trip trip;
        public string startAddress;
        public string endAddress;
        public ArrayList coordinatesString;
        public TripDataContext(Trip t, string sa, string ea)
        {
            this.Trip = t;
            this.StartAddress = sa;
            this.EndAddress = ea;
            this.coordinatesString = new ArrayList();
            this.coordinatesString.Add(t.StartLocation.Latitude.ToString() + "," + t.StartLocation.Lagnitude.ToString());
            this.coordinatesString.Add(t.EndLocation.Latitude.ToString() + "," + t.EndLocation.Lagnitude.ToString());
        }

        public Trip Trip
        {
            get => trip;
            set
            {
                if (value != trip)
                {
                    trip = value;
                    this.StartAddress = trip.StartLocation.Address;
                    this.EndAddress = trip.EndLocation.Address;
                    OnPropertyChanged("Trip");
                };
            }
        }

        public string StartAddress
        {
            get => startAddress;
            set
            {
                if (value != startAddress)
                {
                    startAddress = value;
                    OnPropertyChanged("StartAddress");
                }
            }
        }

        public string EndAddress
        {
            get => endAddress;
            set
            {
                if (value != endAddress)
                {
                    endAddress = value;
                    OnPropertyChanged("EndAddress");
                }
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
