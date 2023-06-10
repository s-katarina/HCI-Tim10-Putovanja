﻿using HCI_Tim10_Putovanja.Core;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace HCI_Tim10_Putovanja
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {

        public HelpViewer(string key)
        {
            InitializeComponent();

            string name = "";
            string id = "";
            
            if (key.Contains("#"))
            {
                name = key.Split("#")[0];
                id = key.Split("#")[1];
            }
            else
                name = key;

            string path = Database.helpFolderPath + name + ".htm";
            MessageBox.Show(path);
            if (!File.Exists(path))
            {
                name = "error";
            }
            Uri u = new Uri(Database.helpFolderPath + name + ".htm#" + id);
            wbHelp.Navigate(u);
        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbHelp != null) && (wbHelp.CanGoBack));
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((wbHelp != null) && (wbHelp.CanGoForward));
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoForward();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}
