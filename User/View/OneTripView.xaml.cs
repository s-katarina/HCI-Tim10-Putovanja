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
	/// Interaction logic for OneTripView.xaml
	/// </summary>
	public partial class OneTripView : Page
	{
		private Trip trip;
		public static RoutedCommand MyCommand = new();

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
			MyCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
		}

		private void Bye_Click(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show("Da li zelite da zavrsite kupovinu " + Trip.Name + "?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
			{
				Debug.WriteLine("no");
			}
			else
			{
				Debug.WriteLine("yes");
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
				Debug.WriteLine("yes");
				MessageBox.Show("Uspesno ste rezervisali novo putovanje!", "Cestitamo", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Instantiate the page to navigate to
			OneAtraction page = new OneAtraction((Atraction)atractionListBox.SelectedItem);

			// Navigate to the page, using the NavigationService
			this.NavigationService.Navigate(page);
		}
	}

	public class MyViewModel
	{
		private ICommand byeShortcut;
		public ICommand ByeShortcut => byeShortcut
					?? (byeShortcut = new ActionCommand(() =>
					{
						MessageBox.Show("SomeCommand");
					}));
	}
	public class ActionCommand : ICommand
	{
		private readonly Action _action;

		public ActionCommand(Action action)
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
}
