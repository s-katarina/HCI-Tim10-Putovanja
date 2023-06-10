using HCI_Tim10_Putovanja.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for UserTrips.xaml
    /// </summary>
    public partial class UserTrips : Page
    {
        public UserTrips()
        {
            InitializeComponent();
            DataContext = this;
        }

        private ObservableCollection<Trip> trips = new();
        public ObservableCollection<Trip> Trips { get => trips; set => trips = value; }

        private List<Trip> GetBoughtTrips()
        {
            List<Trip> ret = new();
            foreach (Record record in Database.SoldTrips)
                if (record.User.Email == Database.loggedInUser.Email)
                    ret.Add(record.Trip);
            return ret;
        }

        private List<Trip> GetReservedTrips()
        {
            List<Trip> ret = new();
            foreach (Record record in Database.ReservedTrips)
                if (record.User.Email == Database.loggedInUser.Email)
                    ret.Add(record.Trip);
            return ret;
        }

        private void FillTrips(object sender, RoutedEventArgs e)
        {
            Trips.Clear();
            if (kupljeniCheckBox.IsChecked == true)
                foreach (Trip trip in GetBoughtTrips())
                    Trips.Add(trip);
            if (rezervisaniCheckBox.IsChecked == true)
                foreach (Trip trip in GetReservedTrips())
                    Trips.Add(trip);
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
