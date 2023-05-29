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

namespace HCI_Tim10_Putovanja.User.View
{
	/// <summary>
	/// Interaction logic for AllTrips.xaml
	/// </summary>
	public partial class AllTrips : Page
	{
		private List<Trip> trips;
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
		}

		public List<Trip> Trips { get => trips; set => trips = value; }

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Debug.WriteLine(tripsListBox.SelectedItem.ToString());
			Debug.WriteLine("Holamibebebe");
			// Instantiate the page to navigate to
			OneTripView page = new OneTripView((Trip)tripsListBox.SelectedItem);

			// Navigate to the page, using the NavigationService
			this.NavigationService.Navigate(page);
		}
	}
}
