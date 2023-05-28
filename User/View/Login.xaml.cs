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
		private Database database = new Database();

		public Login()
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

		public string ErrorMessagee { get; private set; }

		public void Login_Click(object sender, RoutedEventArgs e) {
			if (userName == null || userName.Length < 2 || Password == null || Password.Length < 5)
			{
				MessageBox.Show("Molimo vas popunite sva polja.", "Obustavljena prijava", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			foreach (AppUser user in database.Users) {
				if (user.Email == userName && user.Password == Password) {
					MessageBox.Show("Uspesna prijava.", "Uspesna prijava", MessageBoxButton.OK, MessageBoxImage.Information);
					return;
				}
			}
			MessageBox.Show("Netacni kredincijali.", "Obustavljena prijava", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			Password = ((PasswordBox)sender).Password;
			ErrorMessagee = Password.Length < 5 ? "Lozinka mora imati bar 5 karaktera" : "";
			
			passwordErrorTextBlock.Text = ErrorMessagee;
		}
	}
}
