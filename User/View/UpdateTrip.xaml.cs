﻿using System;
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
    /// Interaction logic for UpdateTrip.xaml
    /// </summary>
    public partial class UpdateTrip : Page
    {
        public UpdateTrip()
        {
            InitializeComponent();
        }

        public UpdateTrip(Trip t)
        {
            InitializeComponent();
            DataContext = t;
        }
    }
}
