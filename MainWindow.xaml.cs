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
            MainFrame.Content = new Reports();
		}
        private void Login_Click(object sender, RoutedEventArgs e)
        {
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

    }
}
