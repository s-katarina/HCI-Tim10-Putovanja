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
using System.Windows.Shapes;

namespace HCI_Tim10_Putovanja.User.View
{
    /// <summary>
    /// Interaction logic for UpdateAttraction.xaml
    /// </summary>
    public partial class UpdateAttraction : Page
    {
        public UpdateAttraction()
        {
            InitializeComponent();
        }

        public UpdateAttraction(Atraction atraction)
        {
            InitializeComponent();
            DataContext = atraction;
        }

    }
}
