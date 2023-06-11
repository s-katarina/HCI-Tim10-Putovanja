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
		private DateTime startTime;
		private DateTime endTime;
		private Location startLocation;
		private Location endLocation;
		private string description;
		private List<Atraction> atractions;
		private List<TuristicStops> touristicStops;

		public override string ToString()
		{
			return this.name;
		}

		public Trip()
		{
			atractions = new List<Atraction>();
			touristicStops = new List<TuristicStops>();
			startLocation = new Location(0, 0, "");
			endLocation = new Location(0, 0, "");
		}

		public Trip(string name, double price, DateTime strartTime, DateTime endTime, Location startLocation, Location endLocation, string description, List<Atraction> atractions, List<TuristicStops> ts)
		{
			this.name = name;
			this.price = price;
			this.startTime = strartTime;
			this.endTime = endTime;
			this.StartLocation = startLocation;
			this.endLocation = endLocation;
			this.description = description;
			this.atractions = atractions;
			this.touristicStops = ts;
		}

		public Trip(Trip t, Location startLocation, bool isStart)
		{
			this.name = t.name;
			this.price = t.price;
			this.startTime = t.startTime;
			this.endTime = t.endTime;
			this.StartLocation = startLocation;
			this.endLocation = t.endLocation;
			this.description = t.description;
			this.atractions = t.atractions;
			this.touristicStops = t.touristicStops;
			Console.WriteLine(t.startLocation.Address);
		}

		public Trip(Trip t, Location endLocation)
		{
			this.name = t.name;
			this.price = t.price;
			this.startTime = t.startTime;
			this.endTime = t.endTime;
			this.StartLocation = t.StartLocation;
			this.endLocation = endLocation;
			this.description = t.description;
			this.atractions = t.atractions;
			this.touristicStops = t.touristicStops;
			Console.WriteLine(t.endLocation.Address);
		}

		public double Price { get => price; set => price = value; }
		public DateTime StartTime { get => startTime; set => startTime = value; }
		public DateTime EndTime { get => endTime; set => endTime = value; }
		public string Description { get => description; set => description = value; }
		public Location EndLocation { get => endLocation; set => endLocation = value; }
		public List<Atraction> Atractions { get => atractions; set => atractions = value; }
		public List<TuristicStops> TouristicStops { get => touristicStops; set => touristicStops = value; }
		public Location StartLocation { get => startLocation; set => startLocation = value; }
		public string Name { get => name; set => name = value; }
	}
}
