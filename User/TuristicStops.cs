using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.User
{
	//restorani i smestaji ista je klasa
	public class TuristicStops
	{
		private string name;
		private Location location;

		public TuristicStops(string name, Location location)
		{
			this.name = name;
			this.location = location;
		}

		public string Name { get => name; set => name = value; }
		public Location Location { get => location; set => location = value; }
	}
}
