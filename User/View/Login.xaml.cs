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

namespace HCI_Tim10_Putovanja.User.View
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Page
	{
		private ObservableObject observableObject = new ObservableObject();

		public Login()
		{
			InitializeComponent();
			DataContext = this;
		}

		private string userName;
		public string UserName
		{
			get => userName;
			set
			{
				if (value != userName)
				{
					userName = value;
					observableObject.OnPropertyChanged();
				}
			}
		}
		private string password;
		public string Password
		{
			get => password;
			set
			{
				if (value != password)
				{
					password = value;
					observableObject.OnPropertyChanged();
				}
			}
		}

		public void Login_Click(object sender, RoutedEventArgs e) { }
	}
}
