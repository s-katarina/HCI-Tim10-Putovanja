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
    /// Interaction logic for AllTouristicStops.xaml
    /// </summary>
    public partial class AllTouristicStops : Page
    {
        static List<TuristicStops> turisticStops;
        public AllTouristicStops()
        {
            InitializeComponent();
        }

        public AllTouristicStops(List<TuristicStops> ts)
        {
            InitializeComponent();
            turisticStops = ts;
            DataContext = this;
        }
        public static List<TuristicStops> TouristicStops { get => turisticStops;  }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(touristicStopsListBox.SelectedItem.ToString());
            // Instantiate the page to navigate to
            UpdateTouristicStop page = new UpdateTouristicStop((TuristicStops)touristicStopsListBox.SelectedItem);

            // Navigate to the page, using the NavigationService
            this.NavigationService.Navigate(page);
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            CreateTouristicStop page = new CreateTouristicStop();
            this.NavigationService.Navigate(page);
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
