using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.User
{
	class User
	{
		private string name;
		private string lastname;
		private string email;
		private string phone;
		private string password;
		private Role role;

		public User(string name, string lastname, string email, string phone, string password, Role role)
		{
			this.name = name;
			this.lastname = lastname;
			this.email = email;
			this.phone = phone;
			this.password = password;
			this.role = role;
		}

		public User()
		{
		}

		public string Name { get => name; set => name = value; }
		public string Lastname { get => lastname; set => lastname = value; }
		public string Email { get => email; set => email = value; }
		public string Password { get => password; set => password = value; }
		public string Phone { get => phone; set => phone = value; }
		internal Role Role { get => role; set => role = value; }
	}

	enum Role { 
		PASSENGER,
		AGENT
	}
}
