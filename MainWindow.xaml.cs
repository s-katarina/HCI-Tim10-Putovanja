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
            List<String> images = new List<string>();
            images.Add("\\constants\\logo2.jpg");
            images.Add("\\constants\\logo2.jpg");
            images.Add("\\constants\\logo2.jpg");
            images.Add("\\constants\\logo2.jpg");
            images.Add("\\constants\\logo2.jpg");
            images.Add("\\constants\\logo2.jpg");
            images.Add("\\constants\\logo2.jpg");
            images.Add("\\constants\\logo2.jpg");
            List<Atraction> atractions = new List<Atraction>();
            atractions.Add(new Atraction("atrakcija1", new Location(0.0, 0.0, "adresaa"), "Holla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", images));
            atractions.Add(new Atraction("atrakcija3", new Location(0.0, 0.0, "adresaa"), "Holla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", images));
            atractions.Add(new Atraction("atrakcija2", new Location(0.0, 0.0, "adresaa"), "Holla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", null));
            atractions.Add(new Atraction("atrakcija4", new Location(0.0, 0.0, "adresaa"), "Holla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", null));
            List<TuristicStops> restar = new List<TuristicStops>();
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            restar.Add(new TuristicStops("restoran", new Location(0.0, 0.0, "asjhks")));
            List<TuristicStops> acom = new List<TuristicStops>();
            acom.Add(new TuristicStops("hoetell", new Location(0.0, 0.0, "asjhks")));
            acom.Add(new TuristicStops("hoetell", new Location(0.0, 0.0, "asjhks")));
            acom.Add(new TuristicStops("hoetell", new Location(0.0, 0.0, "asjhks")));
            acom.Add(new TuristicStops("Tiski cvet", new Location(0.0, 0.0, "Novi sad, bulevar oslobodjenja 4")));
            List<Trip> trips = new List<Trip>();
            trips.Add(new Trip("Carska baraaa", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "polazak"), new Location(44.0, 23.0, "dolayak"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", atractions, restar, acom));
            trips.Add(new Trip("Ovcar planina", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "polazak"), new Location(44.0, 23.0, "dolayak"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", atractions, restar, acom));
            trips.Add(new Trip("Carska baraaa", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "polazak"), new Location(44.0, 23.0, "dolayak"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", atractions, restar, acom));
            trips.Add(new Trip("Carska baraaa", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "polazak"), new Location(44.0, 23.0, "dolayak"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", atractions, restar, acom));
            trips.Add(new Trip("Carska baraaa", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "polazak"), new Location(44.0, 23.0, "dolayak"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", atractions, restar, acom));

            MainFrame.Content = new AllTrips(trips);
            //MainFrame.Content = new OneTripView(new Trip(10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "polazak"), new Location(44.0, 23.0, "dolayak"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", atractions, restar, acom));
        }

        private void TouristicStops_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AllTouristicStops(dbKata.TouristicStops);
        }
        private void Attractions_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AllAttractions(dbKata.Attractions);
        }

    }
}
