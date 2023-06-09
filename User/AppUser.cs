using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.User
{
	class AppUser
	{
		private string name;
		private string lastname;
		private string email;
		private string phone;
		private string password;
		private Role role;
		private List<Trip> reservedTrip;
		private List<Trip> boughtTrip;

		public AppUser(string name, string lastname, string email, string phone, string password, Role role)
		{
			this.name = name;
			this.lastname = lastname;
			this.email = email;
			this.phone = phone;
			this.password = password;
			this.role = role;
			reservedTrip = new List<Trip>();
			boughtTrip = new List<Trip>();

		}

		public AppUser(string name, string lastname, string email, string phone, string password, Role role, List<Trip> reservedTrip, List<Trip> boughtTrip) : this(name, lastname, email, phone, password, role)
		{
			this.reservedTrip = reservedTrip;
			this.boughtTrip = boughtTrip;
		}

		public void AddReservedTrip(Trip trip) {
			this.reservedTrip.Add(trip);
		}

		public void AddBoughtTrip(Trip trip)
		{
			this.boughtTrip.Add(trip);
		}

		public AppUser()
		{
		}

		public string Name { get => name; set => name = value; }
		public string Lastname { get => lastname; set => lastname = value; }
		public string Email { get => email; set => email = value; }
		public string Password { get => password; set => password = value; }
		public string Phone { get => phone; set => phone = value; }
		internal Role Role { get => role; set => role = value; }
		public List<Trip> ReservedTrip { get => reservedTrip; set => reservedTrip = value; }
		public List<Trip> BoughtTrip { get => boughtTrip; set => boughtTrip = value; }
	}

	enum Role { 
		PASSENGER,
		AGENT
	}
}
