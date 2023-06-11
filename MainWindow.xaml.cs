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
        public static RoutedCommand HelpCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            MapService.SetUpService();
            MainFrame.Content = new Login();
            HelpCommand.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("Indeks#");
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
		{
            if (logedoutGrid.Visibility != Visibility.Visible) { return; }
            MainFrame.Content = new Registration();
		}
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (logedoutGrid.Visibility != Visibility.Visible) { return; }
            MainFrame.Content = new Login();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (logedoutGrid.Visibility == Visibility.Visible) { return; }
            Database.loggedInUser = null;
            userGrid.Visibility = Visibility.Hidden;
            agentGrid.Visibility = Visibility.Hidden;
            logedoutGrid.Visibility = Visibility.Visible;
            MainFrame.Content = new Login();

        }

        private void Trips_Click(object sender, RoutedEventArgs e) {
            MainFrame.Content = new AllTrips(Database.Trips);
        }

        private void TouristicStops_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            MainFrame.Content = new AllTouristicStops(Database.TouristicStops);
        }
        private void Attractions_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            MainFrame.Content = new AllAttractions(Database.Attractions);
        }

        private void Users_Trip_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.Visibility != Visibility.Visible) { return; }

            MainFrame.Content = new UserTrips();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            MainFrame.Content = new MonthReport();
        }
        private void TripReport_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            MainFrame.Content = new TripReport();
        }

        internal void ChangeNavbar(Role role)
		{
            logedoutGrid.Visibility = Visibility.Hidden;
            MainFrame.Content = new AllTrips(Database.Trips);
            if (role == Role.AGENT)
            {
                agentGrid.Visibility = Visibility.Visible;
            }
            else {
                userGrid.Visibility = Visibility.Visible;
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
