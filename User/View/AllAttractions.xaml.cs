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

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for AllAttractions.xaml
    /// </summary>
    public partial class AllAttractions : Page
    {
        static List<Atraction> attractions;
        public static List<Atraction> Attractions { get => attractions; }
        public AllAttractions(List<Atraction> atrs)
        {
            InitializeComponent();
            attractions = atrs;
            DataContext = this;
        }

        // private string descShort;
        // public string DescShort { get => descShort; }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Instantiate the page to navigate to
            UpdateAttraction page = new UpdateAttraction((Atraction)attractionsListBox.SelectedItem);

            // Navigate to the page, using the NavigationService
            this.NavigationService.Navigate(page);
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            CreateAttraction page = new CreateAttraction();
            this.NavigationService.Navigate(page);
        }

    }
}
