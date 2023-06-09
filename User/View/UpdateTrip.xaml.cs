﻿using HCI_Tim10_Putovanja.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        public UpdateTrip()
        {
            InitializeComponent();
        }

        public UpdateTrip(Trip t)
        {
            trip = t;
            InitializeComponent();
            DataContext = t;
            TouristicStops = new ArrayList();
            cbAttractionsItems = LoadCbAttractionsData();
            cbTouristicStopsItems = LoadCbTouristicStopsData();
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

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da izmenite? Kliknite OK za potvrdu.", "Potvrda izmena", System.Windows.MessageBoxButton.OK);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                if (txtName.Text == null || txtStartTime.Text == null || txtEndTime.Text == null 
                    || txtStartLocation.Text == null || txtEndLocation == null || txtPrice.Text == null 
                    || txtDesc.Text == null)
                {
                    MessageBox.Show("Molimo vas popunite sva polja", "Neuspesna izmena", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                trip.Name = txtName.Text;
                trip.StartTime = DateTime.Parse(txtStartTime.Text);
                trip.EndTime = DateTime.Parse(txtEndTime.Text);
                trip.StartLocation = new Location(0,0,txtStartLocation.Text);
                trip.EndLocation = new Location(0,0, txtEndLocation.Text);
                trip.Price = double.Parse(txtPrice.Text);
                trip.Description = txtDesc.Text;
                MessageBox.Show("Uspesno izmenjeno putovanje!", "Uspesna izmena", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da obrisete putovanje? Kliknite OK za potvrdu.", "Potvrda brisanja", System.Windows.MessageBoxButton.OK, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                Database.Trips.Remove(trip);
                MessageBox.Show("Uspesno obrisano putovanje!", "Uspesno brisanje", MessageBoxButton.OK, MessageBoxImage.Information);
                AllTrips page = new AllTrips(Database.Trips);
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

        void Pin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            SelectedPushpin = sender as Pushpin;
            _dragPin = true;
            _mouseToMarker = Point.Subtract(
              myMap.LocationToViewportPoint(SelectedPushpin.Location),
              e.GetPosition(myMap));
        }

        void Pin_MouseUp(object sender, MouseButtonEventArgs e)
        {
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