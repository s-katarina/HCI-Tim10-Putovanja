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
		private Boolean isRestaurant = false;
		public TuristicStops(string name, Location location)
		{
			this.name = name;
			this.location = location;
		}

		public TuristicStops(string name, Location location, Boolean isr)
		{
			this.name = name;
			this.location = location;
			this.isRestaurant = isr;
		}

		public string Name { get => name; set => name = value; }
		public Location Location { get => location; set => location = value; }
		public Boolean IsRestaurant { get => isRestaurant; set => isRestaurant = value; }

	}
}
