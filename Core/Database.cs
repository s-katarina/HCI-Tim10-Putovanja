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
		private static List<AppUser> users = new List<AppUser>();

		public static List<AppUser> Users { get => users; set => users = value; }

		private static List<TuristicStops> touristicStops = new List<TuristicStops>();
		public static List<TuristicStops> TouristicStops { get => touristicStops;  }

		private static List<Atraction> atractions = new List<Atraction>();
		public static List<Atraction> Attractions { get => atractions; }

		private static List<Trip> trips = new List<Trip>();
		public static List<Trip> Trips { get => trips; }

		private static List<Record> soldTrips = new List<Record>();
		public static List<Record> SoldTrips { get => soldTrips; }

		private static List<Record> reservedTrips = new List<Record>();
		public static List<Record> ReservedTrips { get => reservedTrips; }

		public static AppUser loggedInUser;
		public static string helpFolderPath = "D:/Faks/3. Godina/Interakcija covek racunar/HCI-Tim10-Putovanja/Help/";


		public Database()
		{

			if (users.Count == 0) { //ako nema inicijalizuje se
				users.Add(new AppUser("Pera", "peric", "pera@gmail.com", "e22212120", "22222", Role.AGENT));
				users.Add(new AppUser("Nina", "peric", "nina@gmail.com", "e22212120", "22222", Role.PASSENGER));
				users.Add(new AppUser("Mika", "peric", "mika@gmail.com", "e22212120", "22222", Role.PASSENGER));
				loggedInUser = users[0];

				touristicStops.Add(new TuristicStops("Restoran Urma", new Location(43.620574, 22.34942, "Yitm Rd 12, Maroko")));
				touristicStops.Add(new TuristicStops("Restoran Bellagio", new Location(43.620574, 22.34942, "Svetosavska 17A, Novi Beograd")));
				touristicStops.Add(new TuristicStops("Hotel Orijan", new Location(43.620574, 22.34942, "Via Pella 91, Rim")));
				touristicStops.Add(new TuristicStops("Motel 021", new Location(42.620574, 21.34942, "Pavla Papa 40, Novi Sad")));

				List<String> images = new List<string>();
				images.Add("\\constants\\logo2.jpg");
				images.Add("\\constants\\logo2.jpg");
				images.Add("\\constants\\logo2.jpg");
				images.Add("\\constants\\logo2.jpg");
				List<String> images2 = new List<string>();
				images2.Add("\\constants\\logo2.jpg");
				images2.Add("\\constants\\logo2.jpg");
				atractions.Add(new Atraction("Mountain View and Hike", new Location(0.0, 0.0, "Alpine Road 78, Uruguay"), "HbebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", images));
				atractions.Add(new Atraction("Krstarenje", new Location(0.0, 0.0, "Pristaniste Dunav @ Beli Brod"), "Holla mibebebebebHolla mibebebebebHolla mibebebebebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", images2));
				atractions.Add(new Atraction("Rafting", new Location(0.0, 0.0, "Kanjon reke Tare"), "Ho mibebebebebHolla mibebebebebHolla mibebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebebHolla mibebebebeb", null));
				atractions.Add(new Atraction("Parachuting", new Location(0.0, 0.0, "Avala 12, Beograd"), "HbeebHolla mibebebebebHolla mibebebebeb", null));

				List<Atraction> a1 = atractions.GetRange(0, 1);
				List<Atraction> a2 = atractions.GetRange(1, 2);
				List<Atraction> a3 = atractions.GetRange(1, 3);
				List<Atraction> a4 = atractions.GetRange(0, 3);

				List<TuristicStops> t1 = touristicStops.GetRange(0, 2);
				List<TuristicStops> t2 = touristicStops.GetRange(1, 2);
				List<TuristicStops> t3 = touristicStops.GetRange(1, 3);

				trips.Add(new Trip("Carska bara", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 21.0, "Parking kod Master Centra, Novi Sad"), new Location(44.0, 23.0, "Zeleznicka stanica, Novi Sad"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", a1, t1));
				trips.Add(new Trip("Ovcar planina", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "Beograd BAS"), new Location(44.0, 23.0, "Parking kod Master Centra, Novi Sad"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", a2, t2));
				trips.Add(new Trip("Avantura na Tari", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "Zeleznicka stanica, Novi Sad"), new Location(44.0, 23.0, "Autobuska stanica, Novi Sad"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", a3, t3));
				trips.Add(new Trip("Prelepi pogledi kanjona Uvca", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "Autobuska stanica, Novi Sad"), new Location(44.0, 23.0, "Beograd BAS"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", a4, t1));
				trips.Add(new Trip("Deliblatska pescara", 10000.0, DateTime.Now, DateTime.Now, new Location(44.0, 23.0, "Autobuska stanica, Novi Sad"), new Location(44.0, 23.0, "Beograd BAS"), " have several Classes that contain Classes and need to access their properties in the WPF Forms. I am trying to Bind properties of ppl to controls. Using Path=ppl.wife apparently is not correct. (obviously I am new to WPF)", a1, t3));

				soldTrips.Add(new Record(users[1], trips[0], new DateTime(2023, 8, 20)));
				reservedTrips.Add(new Record(users[1], trips[1], new DateTime(2023, 12, 20)));
				reservedTrips.Add(new Record(users[1], trips[2], new DateTime(2023, 3, 20)));
				soldTrips.Add(new Record(users[2], trips[0], new DateTime(2023, 3, 20)));
				soldTrips.Add(new Record(users[2], trips[1], new DateTime(2023, 3, 20)));
			}
		}

		public static void AddUser(AppUser appUser) {
			users.Add(appUser);
		}
	}
}
