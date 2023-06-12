using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
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
using Microsoft.Maps.MapControl.WPF;

namespace HCI_Tim10_Putovanja.User.View
{
	/// <summary>
	/// Interaction logic for OneTripView.xaml
	/// </summary>
	public partial class OneTripView : Page
	{
		private Trip trip;
		private string startLocation;
		private string endLocation;
		public static RoutedCommand GoBackShortcut = new RoutedCommand();

		public Trip Trip { get => trip; set => trip = value; }
		public string StartLocation { get => startLocation; set => startLocation = value; }
		public string EndLocation { get => endLocation; set => endLocation = value; }

		public OneTripView()
		{
			InitializeComponent();
			DataContext = this;
		}

		public OneTripView(Trip trip) {
			this.Trip = trip;
			this.startLocation = trip.StartLocation.Latitude.ToString() + "," + trip.StartLocation.Lagnitude.ToString();
			this.endLocation = trip.EndLocation.Latitude.ToString() + "," + trip.EndLocation.Lagnitude.ToString();
			InitializeComponent();
			GetRoute();
			DataContext = this;
			Debug.WriteLine(trip.Price);
			GoBackShortcut.InputGestures.Add(new KeyGesture(Key.Enter, ModifierKeys.Shift));

			boughtStack.Visibility = Visibility.Hidden;
			originalStack.Visibility = Visibility.Hidden;
			reservedStack.Visibility = Visibility.Hidden;

			if (Database.loggedInUser != null && Database.loggedInUser.Role.Equals(Role.PASSENGER))
			{
				if (IsReserved())
				{
					reservedStack.Visibility = Visibility.Visible;
				}
				else if (IsBought())
					boughtStack.Visibility = Visibility.Visible;
				else
				{
					originalStack.Visibility = Visibility.Visible;
				}

			}
		}

		private void drawRoute()
		{

			LocationCollection locs = new LocationCollection();

			locs.Add(new Microsoft.Maps.MapControl.WPF.Location(trip.StartLocation.Latitude, trip.StartLocation.Lagnitude));
			locs.Add(new Microsoft.Maps.MapControl.WPF.Location(trip.EndLocation.Latitude, trip.EndLocation.Lagnitude));

			MapPolyline routeLine = new MapPolyline()
			{
				Locations = locs,
				Stroke = new SolidColorBrush(Colors.Blue),
				StrokeThickness = 5
			};

			myMap.Children.Add(routeLine);
		}
		private void Route(string start, string end, string key, Action<Response> callback)
		{
			Uri requestURI = new Uri(string.Format("http://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0={0}&wp.1={1}&rpo=Points&key={2}", Uri.EscapeDataString(start), Uri.EscapeDataString(end), key));
			GetResponse(requestURI, callback);
		}

		private void GetResponse(Uri uri, Action<Response> callback)
		{
			try
			{
				HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					using (Stream stream = response.GetResponseStream())
					{
						DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));

						if (callback != null)
						{
							callback(ser.ReadObject(stream) as Response);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Usao u exception");
				var t = "";
			}
		}
		private void GetKey(Action<string> callback)
		{
			Debug.WriteLine("id");
			if (callback != null)
			{
				myMap.CredentialsProvider.GetCredentials((c) =>
				{
					Debug.WriteLine("id");
					Debug.WriteLine(c.ApplicationId);
					callback(c.ApplicationId);
				});
			}
		}

		private void GetRoute()
		{
			string to = "Seattle";
			string from = "New York";

			if (!string.IsNullOrWhiteSpace(from))
			{
				if (!string.IsNullOrWhiteSpace(to))
				{
					GetKey((c) =>
					{
						Route(from, to, c, (r) =>
						{
							Debug.WriteLine("bbaaaaa");
							Debug.WriteLine(r.TraceId);
							if (r != null &&
								r.ResourceSets != null &&
								r.ResourceSets.Length > 0 &&
								r.ResourceSets[0].Resources != null &&
								r.ResourceSets[0].Resources.Length > 0)
							{
								Route route = r.ResourceSets[0].Resources[0] as Route;

								double[][] routePath = route.RoutePath.Line.Coordinates;
								LocationCollection locs = new LocationCollection();

								for (int i = 0; i < routePath.Length; i++)
								{
									if (routePath[i].Length >= 2)
									{
										locs.Add(new Microsoft.Maps.MapControl.WPF.Location(routePath[i][0], routePath[i][1]));
									}
								}

								MapPolyline routeLine = new MapPolyline()
								{
									Locations = locs,
									Stroke = new SolidColorBrush(Colors.Blue),
									StrokeThickness = 5
								};

								myMap.Children.Add(routeLine);

								myMap.SetView(locs, new Thickness(30), 0);
							}
							else
							{
								MessageBox.Show("No Results found.");
							}
						});
					});
				}
				else
				{
					MessageBox.Show("Invalid End location.");
				}
			}
			else
			{
				MessageBox.Show("Invalid Start location.");
			}
		}

		private bool IsBought()
		{
			foreach (Record r in Database.SoldTrips)
				if (r.User.Email == Database.loggedInUser.Email && r.Trip.Name == trip.Name)
					return true;
			return false;
		}

		private bool IsReserved()
        {
			foreach (Record r in Database.ReservedTrips)
				if (r.User.Email == Database.loggedInUser.Email && r.Trip.Name == trip.Name)
					return true;
			return false;
		}

		private void Bye_Click(object sender, RoutedEventArgs e)
		{
			if (!(reservedStack.Visibility == Visibility.Visible || originalStack.Visibility == Visibility.Visible)) return;
			if (MessageBox.Show("Da li zelite da zavrsite kupovinu " + Trip.Name + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
			{
				Debug.WriteLine("no");
			}
			else
			{
				if (already_bought()) return;
				removeFromReserved();
				reservedStack.Visibility = Visibility.Hidden;
				originalStack.Visibility = Visibility.Hidden;
				boughtStack.Visibility = Visibility.Visible;
				Debug.WriteLine("yes");
				Database.SoldTrips.Add(new Record(Database.loggedInUser, trip, DateTime.Now));
				MessageBox.Show("Uspesno ste kupili novo putovanje!", "Cestitamo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void removeFromReserved()
		{
			foreach (Record r in Database.ReservedTrips)
				if (r.User.Email == Database.loggedInUser.Email && r.Trip.Name == trip.Name) { 
					Database.ReservedTrips.Remove(r);
					return;
				}

		}

		private void Reserve_Click(object sender, RoutedEventArgs e)
		{
			if (originalStack.Visibility != Visibility.Visible) return;
			if (MessageBox.Show("Da li zelite da rezervisete " + Trip.Name + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
			{
				Debug.WriteLine("no");
			}
			else
			{
				if (already_reserved()) return;
				if (already_bought()) return;
				reservedStack.Visibility = Visibility.Visible;
				originalStack.Visibility = Visibility.Hidden;
				boughtStack.Visibility = Visibility.Hidden;
				Debug.WriteLine("yes");
				Database.ReservedTrips.Add(new Record(Database.loggedInUser, trip, DateTime.Now) );
				MessageBox.Show("Uspesno ste rezervisali novo putovanje!", "Cestitamo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			if (reservedStack.Visibility != Visibility.Visible) return;
			if (MessageBox.Show("Da li sigurno zelite da otkazete rezervaciju?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				reservedStack.Visibility = Visibility.Hidden;
				originalStack.Visibility = Visibility.Visible;
				boughtStack.Visibility = Visibility.Hidden;
				foreach (Record r in Database.ReservedTrips)
					if (r.User.Email == Database.loggedInUser.Email && r.Trip.Name == trip.Name)
					{
						Database.ReservedTrips.Remove(r);
						MessageBox.Show("Uspesno otkazano putovanje!");
						break;
					}
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
