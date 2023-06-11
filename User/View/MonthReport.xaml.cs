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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class MonthReport : Page, INotifyPropertyChanged
    {

        public MonthReport()
        {
            InitializeComponent();
            PopulateYearComboBox();
            PopulateMonthComboBox();
            DataContext = this;
        }

        private ObservableCollection<Record> records = new();
        public ObservableCollection<Record> Records { get => records; set => records = value; }

        private void PopulateYearComboBox()
        {
            for (int year = 2023; year >= 1990; year--)
                YearComboBox.Items.Add(year.ToString());
        }

        private void PopulateMonthComboBox()
        {
            MonthComboBox.Items.Add("Januar");
            MonthComboBox.Items.Add("Februar");
            MonthComboBox.Items.Add("Mart");
            MonthComboBox.Items.Add("April");
            MonthComboBox.Items.Add("Maj");
            MonthComboBox.Items.Add("Jun");
            MonthComboBox.Items.Add("Jul");
            MonthComboBox.Items.Add("Avgust");
            MonthComboBox.Items.Add("Septembar");
            MonthComboBox.Items.Add("Oktobar");
            MonthComboBox.Items.Add("Novembar");
            MonthComboBox.Items.Add("Decembar");
        }

        private void Show_trips(object sender, RoutedEventArgs e)
        {
            int year = GetSelectedYear();
            int month = GetSelectedMonth();
            Records.Clear();
            foreach (Record record in Database.SoldTrips)
                if (month == record.Date.Month && year == record.Date.Year)
                    Records.Add(record);

            //foreach (Trip trip in trips)
            //    MessageBox.Show(trip.Name);
        }

        private int GetSelectedYear()
        {
            string year = (string) YearComboBox.SelectedItem;
            return int.Parse(year);
        }

        private int GetSelectedMonth()
        {
            return MonthComboBox.SelectedIndex + 1;
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
