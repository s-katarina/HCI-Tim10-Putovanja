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
    /// Interaction logic for TripReport.xaml
    /// </summary>
    public partial class TripReport : Page, INotifyPropertyChanged
    {
        public TripReport()
        {
            InitializeComponent();
            PopulateTripComboBox();
            DataContext = this;
        }

        private ObservableCollection<Record> records = new();
        public ObservableCollection<Record> Records { get => records; set => records = value; }

        private void PopulateTripComboBox()
        {
            foreach (Trip trip in Database.Trips)
                TripComboBox.Items.Add(trip.Name);
        }

        private void Show_trips(object sender, RoutedEventArgs e)
        {
            string tripName = GetSelectedTrip();
            MessageBox.Show(tripName);
            Records.Clear();
            foreach (Record record in Database.SoldTrips)
                if (tripName == record.Trip.Name)
                    Records.Add(record);

            //foreach (Record trip in Records)
            //    MessageBox.Show(trip.Trip.Name);
        }

        private string GetSelectedTrip()
        {
            return (string)TripComboBox.SelectedItem;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
