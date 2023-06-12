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
using HCI_Tim10_Putovanja.Core;

namespace HCI_Tim10_Putovanja.User.View
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Page
	{
		private ObservableObject observableObject = new ObservableObject();
		public static RoutedCommand MyCommand = new RoutedCommand();

		public Login()
		{
			InitializeComponent();
			DataContext = this;
			ErrorMessagee = "";
			MyCommand.InputGestures.Add(new KeyGesture(Key.Enter, ModifierKeys.None));
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

			Debug.WriteLine("u loginu2");
			foreach (AppUser user in Database.Users) {
				Debug.WriteLine(user.Email);
				if (user.Email == userName && user.Password == Password) {
					Database.loggedInUser = user;
					((MainWindow)System.Windows.Application.Current.MainWindow).ChangeNavbar(user.Role);
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
