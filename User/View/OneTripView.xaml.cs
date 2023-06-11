using System;
using System.Collections.Generic;
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
using HCI_Tim10_Putovanja.Core;

namespace HCI_Tim10_Putovanja.User.View
{
	/// <summary>
	/// Interaction logic for OneTripView.xaml
	/// </summary>
	public partial class OneTripView : Page
	{
		private Trip trip;
		public static RoutedCommand GoBackShortcut = new RoutedCommand();

		public Trip Trip { get => trip; set => trip = value; }

		public OneTripView()
		{
			InitializeComponent();
			DataContext = this;
		}

		public OneTripView(Trip trip) {
			this.Trip = trip;
			InitializeComponent();
			DataContext = this;
			Debug.WriteLine(trip.Price);
			//GoBackShortcut.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Shift));
			if (Database.loggedInUser == null)
			{
				byeBtn.Visibility = Visibility.Hidden;
				reserveBtn.Visibility = Visibility.Hidden;
			}
		}

		private void Bye_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Da li zelite da zavrsite kupovinu " + Trip.Name + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
			{
				Debug.WriteLine("no");
			}
			else
			{
				if (already_bought()) return;
				Debug.WriteLine("yes");
				Database.SoldTrips.Add(new Record(Database.loggedInUser, trip, DateTime.Now));
				MessageBox.Show("Uspesno ste kupili novo putovanje!", "Cestitamo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void Reserve_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Da li zelite da rezervisete " + Trip.Name + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
			{
				Debug.WriteLine("no");
			}
			else
			{
				if (already_reserved()) return;
				if (already_bought()) return;
				Debug.WriteLine("yes");
				Database.ReservedTrips.Add(new Record(Database.loggedInUser, trip, DateTime.Now) );
				MessageBox.Show("Uspesno ste rezervisali novo putovanje!", "Cestitamo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private bool already_reserved()
		{
			foreach (Record r in Database.ReservedTrips)
			{
				if (r.User.Email == Database.loggedInUser.Email && r.Trip.Name == trip.Name)
				{
					MessageBox.Show("Ovo putovanje je vec rezervisano!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
					return true;
				}
			}
			return false;
		}

		private bool already_bought()
		{
			foreach (Record r in Database.SoldTrips)
			{
				if (r.User.Email == Database.loggedInUser.Email && r.Trip.Name == trip.Name)
				{
					MessageBox.Show("Ovo putovanje je vec kupljeno!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
					return true;
				}
			}
			return false;
		}

		private void Go_Back_Click(object sender, RoutedEventArgs e)
		{
			// Instantiate the page to navigate to
			AllTrips page = new AllTrips(Database.Trips);

			// Navigate to the page, using the NavigationService
			this.NavigationService.Navigate(page);
		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Instantiate the page to navigate to
			OneAtraction page = new OneAtraction((Atraction)atractionListBox.SelectedItem);

			// Navigate to the page, using the NavigationService
			this.NavigationService.Navigate(page);
		}
	}

	
}
