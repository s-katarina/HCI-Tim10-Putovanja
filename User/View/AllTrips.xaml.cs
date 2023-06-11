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
	/// Interaction logic for AllTrips.xaml
	/// </summary>
	public partial class AllTrips : Page
	{
		private List<Trip> trips;
		public static readonly System.Windows.DependencyProperty TabNavigationProperty;
		public static RoutedCommand OneTripShortcut = new RoutedCommand();

		public AllTrips()
		{
			InitializeComponent();
			//mogu i ove da se ucitaju sva putovanja
		}

		public AllTrips(List<Trip> trips)
		{
			InitializeComponent();
			this.trips = trips;
			DataContext = this;
			OneTripShortcut.InputGestures.Add(new KeyGesture(Key.Enter, ModifierKeys.None));

			btnAddNew.Visibility = Visibility.Hidden;
			if (Database.loggedInUser != null && Database.loggedInUser.Role.Equals(Role.AGENT))
				btnAddNew.Visibility = Visibility.Visible;
		}

		public List<Trip> Trips { get => trips; set => trips = value; }

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			OpenOneTrip();
		}
		private void ListBox_Klick(object sender, MouseButtonEventArgs e)
		{
			var item = sender as ListViewItem;
			if (item != null)
			{
				OpenOneTrip();
			}
		}

		private void OpenOneTrip()
		{
			if (Database.loggedInUser == null || Database.loggedInUser.Role.Equals(Role.PASSENGER))
			{
				OneTripView page = new OneTripView((Trip)tripsListBox.SelectedItem);
				this.NavigationService.Navigate(page);
			}
			else if (Database.loggedInUser.Role.Equals(Role.AGENT))
			{
				UpdateTrip page = new UpdateTrip((Trip)tripsListBox.SelectedItem);
				this.NavigationService.Navigate(page);
			}
		}

		private void txtNameToSearch_TextChanged(object sender,
TextChangedEventArgs e)
		{
			string searchName = txtNameToSearch.Text;
			string lower = searchName.ToLower();
			List<Trip> filtered = new();
			foreach (Trip trip in trips) {
				if (trip.Name.ToLower().Contains(lower)) filtered.Add(trip);
			}


			tripsListBox.ItemsSource = filtered;
		}

		private void AddNew_Click(object sender, RoutedEventArgs e)
		{
			CreateTrip page = new CreateTrip();
			this.NavigationService.Navigate(page);
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
