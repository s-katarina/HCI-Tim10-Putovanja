using System;
using System.Collections.Generic;
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
using HCI_Tim10_Putovanja.User;
using HCI_Tim10_Putovanja.User.View;

namespace HCI_Tim10_Putovanja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Database dbKata = new Database();

        public MainWindow()
        {
            InitializeComponent();
            MapService.SetUpService();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
		{
            MainFrame.Content = new UserTrips();
		}
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Login();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Database.loggedInUser = null;
            loginBtn.Visibility = Visibility.Visible;
            registrationBtn.Visibility = Visibility.Visible;
            logoutBtn.Visibility = Visibility.Hidden;
            atractionBtn.Visibility = Visibility.Hidden;
            restorantsBtn.Visibility = Visibility.Hidden;
            reportBtn.Visibility = Visibility.Hidden;
            boughtTripsBtn.Visibility = Visibility.Hidden;
            tripReportBtn.Visibility = Visibility.Hidden;
            MainFrame.Content = new Login();

        }

        private void Trips_Click(object sender, RoutedEventArgs e) {
            
            MainFrame.Content = new AllTrips(Database.Trips);
        }

        private void TouristicStops_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AllTouristicStops(Database.TouristicStops);
        }
        private void Attractions_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AllAttractions(Database.Attractions);
        }

        private void Users_Trip_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new UserTrips();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MonthReport();
        }
        private void TripReport_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TripReport();
        }

        internal void ChangeNavbar(Role role)
		{
            loginBtn.Visibility = Visibility.Hidden;
            registrationBtn.Visibility = Visibility.Hidden;
            logoutBtn.Visibility = Visibility.Visible;
            MainFrame.Content = new AllTrips(Database.Trips);
            if (role == Role.AGENT)
            {
                atractionBtn.Visibility = Visibility.Visible;
                restorantsBtn.Visibility = Visibility.Visible;
                reportBtn.Visibility = Visibility.Visible;
                tripReportBtn.Visibility = Visibility.Visible;
            }
            else { 
                boughtTripsBtn.Visibility = Visibility.Visible;
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
