using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using HCI_Tim10_Putovanja.User;

namespace HCI_Tim10_Putovanja.Core
{
	class Database
	{
		private List<AppUser> users = new List<AppUser>();

		internal List<AppUser> Users { get => users; set => users = value; }

		private List<TuristicStops> touristicStops = new List<TuristicStops>();
		public List<TuristicStops> TouristicStops { get => touristicStops;  }

		private List<Atraction> atractions = new List<Atraction>();
		public List<Atraction> Attractions { get => atractions; }

		public Database()
		{
			users.Add(new AppUser("Pera", "peric", "pera@gmail.com", "e22212120", "22222", Role.AGENT));
			users.Add(new AppUser("Nina", "peric", "nina@gmail.com", "e22212120", "22222", Role.PASSENGER));
			users.Add(new AppUser("Mika", "peric", "mika@gmail.com", "e22212120", "22222", Role.PASSENGER));

			touristicStops.Add(new TuristicStops("Restoran Urma", new Location(0.0, 0.0, "Yitm Rd 12, Maroko")));
			touristicStops.Add(new TuristicStops("Restoran Bellagio", new Location(0.0, 0.0, "Svetosavska 17A, Novi Beograd")));
			touristicStops.Add(new TuristicStops("Hotel Orijan", new Location(0.0, 0.0, "Via Pella 91, Rim")));
			touristicStops.Add(new TuristicStops("Motel 021", new Location(0.0, 0.0, "Pavla Papa 40, Novi Sad")));

			List<String> images = new List<string>();
			images.Add("\\constants\\logo2.jpg");
			images.Add("\\constants\\logo2.jpg");
			images.Add("\\constants\\logo2.jpg");
			images.Add("\\constants\\logo2.jpg");
			atractions.Add(new Atraction("Mountain View and Hike", new Location(0.0, 0.0, "Alpine Road 78, Uruguay"), "HbebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", images));
			atractions.Add(new Atraction("Krstarenje", new Location(0.0, 0.0, "Pristaniste Dunav @ Beli Brod"), "Holla mibebebebebHolla mibebebebebHolla mibebebebebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", images));
			atractions.Add(new Atraction("Rafting", new Location(0.0, 0.0, "Kanjon reke Tare"), "Ho mibebebebebHolla mibebebebebHolla mibebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", null));
			atractions.Add(new Atraction("Parachuting", new Location(0.0, 0.0, "Avala 12, Beograd"), "HbeebHolla mibebebebebHolla mibebebebeb", null));

		}

		public void AddUser(AppUser appUser) {
			users.Add(appUser);
		}
	}
}
