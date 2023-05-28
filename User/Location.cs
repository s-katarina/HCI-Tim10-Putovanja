using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.User
{
	public class Location
	{
		private double latitude;
		private double lagnitude;
		private string address;

		public Location(double latitude, double lagnitude, string address)
		{
			Latitude = latitude;
			Lagnitude = lagnitude;
			Address = address;
		}

		public Location()
		{
		}

		public double Latitude { get => latitude; set => latitude = value; }
		public double Lagnitude { get => lagnitude; set => lagnitude = value; }
		public string Address { get => address; set => address = value; }
	}
}
