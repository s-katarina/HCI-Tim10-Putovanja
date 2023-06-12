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
        private readonly Brush buttonColor = Brushes.LightBlue;
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
            if (Database.loggedInUser != null && Database.loggedInUser.Role == Role.AGENT)
            {
                HelpProvider.ShowHelp("Indeks#");
            } else
            {
                HelpProvider.ShowHelp("Indeks_User#");
            }

        }
        private void AllButtonsTransparent() {
            travel1btn.Background = Brushes.Transparent;
            registrationBtn.Background = Brushes.Transparent;
            travel2btn.Background = Brushes.Transparent;
            boughtbtn.Background = Brushes.Transparent;
            travel3btn.Background = Brushes.Transparent;
            atractionsbtn.Background = Brushes.Transparent;
            restorauntsbtn.Background = Brushes.Transparent;
            reportbtn.Background = Brushes.Transparent;
            tripReportbtn.Background = Brushes.Transparent;

        }

        private void Registration_Click(object sender, RoutedEventArgs e)
		{
            if (logedoutGrid.Visibility != Visibility.Visible) { return; }
            AllButtonsTransparent();
            registrationBtn.Background = buttonColor;
            MainFrame.Content = new Registration();
		}
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (logedoutGrid.Visibility != Visibility.Visible) { return; }
            AllButtonsTransparent();

            MainFrame.Content = new Login();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (logedoutGrid.Visibility == Visibility.Visible) { return; }
            Database.loggedInUser = null;
            userGrid.Visibility = Visibility.Hidden;
            agentGrid.Visibility = Visibility.Hidden;
            logedoutGrid.Visibility = Visibility.Visible;
            AllButtonsTransparent();
            MainFrame.Content = new Login();

        }

        private void Trips_Click(object sender, RoutedEventArgs e) {
            AllButtonsTransparent();
            travel1btn.Background = buttonColor;
            travel2btn.Background = buttonColor;
            travel3btn.Background = buttonColor;
            MainFrame.Content = new AllTrips(Database.Trips);
        }

        private void TouristicStops_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            AllButtonsTransparent();
            restorauntsbtn.Background = buttonColor;

            MainFrame.Content = new AllTouristicStops(Database.TouristicStops);
        }
        private void Attractions_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            AllButtonsTransparent();
            atractionsbtn.Background = buttonColor;
            MainFrame.Content = new AllAttractions(Database.Attractions);
        }

        private void Users_Trip_Click(object sender, RoutedEventArgs e)
        {
            if (userGrid.Visibility != Visibility.Visible) { return; }
            AllButtonsTransparent();
            boughtbtn.Background = buttonColor;
            MainFrame.Content = new UserTrips();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            AllButtonsTransparent();
            reportbtn.Background = buttonColor;
            MainFrame.Content = new MonthReport();
        }
        private void TripReport_Click(object sender, RoutedEventArgs e)
        {
            if (agentGrid.Visibility != Visibility.Visible) { return; }
            AllButtonsTransparent();
            tripReportbtn.Background = buttonColor;
            MainFrame.Content = new TripReport();
        }

        internal void ChangeNavbar(Role role)
		{
            logedoutGrid.Visibility = Visibility.Hidden;
            MainFrame.Content = new AllTrips(Database.Trips);
            AllButtonsTransparent();
            if (role == Role.AGENT)
            {
                travel3btn.Background = buttonColor;

                agentGrid.Visibility = Visibility.Visible;
            }
            else {
                travel2btn.Background = buttonColor;
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
