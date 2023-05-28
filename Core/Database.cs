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

		public Database()
		{
			users.Add(new AppUser("Pera", "peric", "pera@gmail.com", "e22212120", "22222", Role.AGENT));
			users.Add(new AppUser("Nina", "peric", "nina@gmail.com", "e22212120", "22222", Role.PASSENGER));
			users.Add(new AppUser("Mika", "peric", "mika@gmail.com", "e22212120", "22222", Role.PASSENGER));
		}

		public void AddUser(AppUser appUser) {
			users.Add(appUser);
		}
	}
}
