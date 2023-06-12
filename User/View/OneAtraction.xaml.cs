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
	/// Interaction logic for OneAtraction.xaml
	/// </summary>
	public partial class OneAtraction : Page
	{
		private Atraction atraction;
		private string mapLocation;
		public OneAtraction()
		{
			InitializeComponent();
		}
		public OneAtraction(Atraction atraction_)
		{
			InitializeComponent();
			this.Atraction = atraction_;
			mapLocation = atraction.Location.Latitude.ToString() + "," + atraction.Location.Lagnitude.ToString();
			DataContext = this;
		}

		public Atraction Atraction { get => atraction; set => atraction = value; }
		public string MapLocation { get => mapLocation; set => mapLocation = value; }
	}
}
