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
using HCI_Tim10_Putovanja.User;
using HCI_Tim10_Putovanja.User.View;

namespace HCI_Tim10_Putovanja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void Registration_Click(object sender, RoutedEventArgs e)
		{
            MainFrame.Content = new Registration();
		}
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Login();
        }

        private void Trips_Click(object sender, RoutedEventArgs e) {
            List<Atraction> atractions = new List<Atraction>();
            atractions.Add(new Atraction(new Location(0.0, 0.0, "adresaa"), "ssssss00", null));
            atractions.Add(new Atraction(new Location(0.0, 0.0, "adresaa"), "ssssss00", null));
            atractions.Add(new Atraction(new Location(0.0, 0.0, "adresaa"), "ssssss00", null));
            atractions.Add(new Atraction(new Location(0.0, 0.0, "adresaa"), "ssssss00", null));
            List<TuristicStops> restar = new List<TuristicStops>();
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            List<TuristicStops> acom = new List<TuristicStops>();
            acom.Add(new TuristicStops("hoetell", new Location(0.0, 0.0, "asjhks")));
            acom.Add(new TuristicStops("hoetell", new Location(0.0, 0.0, "asjhks")));
            acom.Add(new TuristicStops("hoetell", new Location(0.0, 0.0, "asjhks")));
            acom.Add(new TuristicStops("hoetell", new Location(0.0, 0.0, "asjhks")));

            MainFrame.Content = new OneTripView(new Trip(10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "polazak"), new Location(44.0, 23.0, "dolayak"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", atractions, restar, acom));
        }
    }
}
