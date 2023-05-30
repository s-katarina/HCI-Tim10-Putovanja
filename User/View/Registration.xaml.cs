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

namespace HCI_Tim10_Putovanja.User.View
{
	/// <summary>
	/// Interaction logic for Registration.xaml
	/// </summary>
	public partial class Registration : Page
	{
		public bool disabled;
		public SecureString SecurePassword { private get; set; }
		private ObservableObject observableObject = new ObservableObject();
		private Database database = new Database();
		public Registration()
		{
			InitializeComponent();
			DataContext = this;
			ErrorMessagee = "";
			disabled = true;
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

		

		/*public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}*/

		private void Register_Click(object sender, RoutedEventArgs e)
		{
			if (userEmail == null || userName == null || userLastname == null|| userPhone == null || userPassword==null || userPassword.Length < 5) {
				MessageBox.Show("Molimo vas popunite sva polja.", "Obustavljena registracija", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			database.AddUser(new AppUser(UserName, UserLastname, UserEmail, userPhone, UserPassword, Role.PASSENGER));
			MessageBox.Show("Uspesno napravljen nalog.", "USPESNA registracija", MessageBoxButton.OK, MessageBoxImage.Information);

		}


		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			disabled = false;
			UserPassword = ((PasswordBox)sender).Password;
			ErrorMessagee = UserPassword.Length < 5 ? "Lozinka mora imati bar 5 karaktera" : "";
			Debug.WriteLine(UserPassword);
			Debug.WriteLine(ErrorMessagee);
			passwordErrorTextBlock.Text = ErrorMessagee;
		}
	}
}
