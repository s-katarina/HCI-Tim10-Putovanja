using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HCI_Tim10_Putovanja.Core;

namespace HCI_Tim10_Putovanja.User
{
	/// <summary>
	/// Interaction logic for Registration.xaml
	/// </summary>
	public partial class Registration : Window
	{
		public string Password { private get; set; }
		public SecureString SecurePassword { private get; set; }
		private ObservableObject observableObject = new ObservableObject();
		public Registration()
		{
			InitializeComponent();
			DataContext = this;
			ErrorMessagee = "";
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
		private string userLastname;
		public string UserLastname
		{
			get => userLastname;
			set
			{
				if (value != userLastname)
				{
					userLastname = value;
					observableObject.OnPropertyChanged();
				}
			}
		}
		private string userEmail;
		public string UserEmail
		{
			get => userEmail;
			set
			{
				if (value != userEmail)
				{
					userEmail = value;
					observableObject.OnPropertyChanged();
				}
			}
		}
		private string userPhone;
		public string UserPhone
		{
			get => userPhone;
			set
			{
				if (value != userPhone)
				{
					userPhone = value;
					observableObject.OnPropertyChanged();
				}
			}
		}
		private string userPassword;
		public string UserPassword
		{
			get => userPassword;
			set
			{
				if (value != userPassword)
				{
					userPassword = value;
					observableObject.OnPropertyChanged();
				}
			}
		}

		public string ErrorMessagee { get; private set; }

		private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
			if (e.LeftButton == MouseButtonState.Pressed) DragMove();
		}

		/*public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}*/

		private void Register_Click(object sender, RoutedEventArgs e)
		{

		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("lozinka ");
			Debug.WriteLine(ErrorMessagee);
			if (DataContext != null)
			{ ((dynamic)DataContext).Password = ((PasswordBox)sender).Password;
				ErrorMessagee = Password.Length < 5 ? "Lozinka mora imati bar 5 karaktera" : "";
			}

			else
			{
				ErrorMessagee = "Lozinka mora imati bar 5 karaktera";
			}
		}
	}
}
