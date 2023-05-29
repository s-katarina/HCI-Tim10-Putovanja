using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.User
{
	public class Trip
	{
		private string name;
		private double price;
		private DateTime strartTime;
		private DateTime endTime;
		private Location startLocation;
		private Location endLocation;
		private string description;
		private List<Atraction> atractions;
		private List<TuristicStops> restaurants;
		private List<TuristicStops> acommodations;

		public override string ToString()
		{
			return this.name;
		}

		public Trip()
		{
			atractions = new List<Atraction>();
			restaurants = new List<TuristicStops>();
			acommodations = new List<TuristicStops>();
		}

		public Trip(string name, double price, DateTime strartTime, DateTime endTime, Location startLocation, Location endLocation, string description, List<Atraction> atractions, List<TuristicStops> restaurants, List<TuristicStops> acommodations)
		{
			this.name = name;
			this.price = price;
			this.strartTime = strartTime;
			this.endTime = endTime;
			this.StartLocation = startLocation;
			this.endLocation = endLocation;
			this.description = description;
			this.atractions = atractions;
			this.restaurants = restaurants;
			this.acommodations = acommodations;
		}

		public double Price { get => price; set => price = value; }
		public DateTime StrartTime { get => strartTime; set => strartTime = value; }
		public DateTime EndTime { get => endTime; set => endTime = value; }
		public string Description { get => description; set => description = value; }
		public Location EndLocation { get => endLocation; set => endLocation = value; }
		public List<Atraction> Atractions { get => atractions; set => atractions = value; }
		public List<TuristicStops> Restaurants { get => restaurants; set => restaurants = value; }
		public List<TuristicStops> Acommodations { get => acommodations; set => acommodations = value; }
		public Location StartLocation { get => startLocation; set => startLocation = value; }
		public string Name { get => name; set => name = value; }
	}
}
