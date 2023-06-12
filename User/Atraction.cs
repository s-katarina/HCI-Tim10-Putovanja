using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.User
{
	public class Atraction
	{
		private string name;
		private Location location;
		private string description;
		private List<string> images;
		private string mainImage;

		public Atraction()
		{
			images = new List<string>();
		}

		public Atraction(string name, Location location, string description, List<string> images)
		{
			this.name = name;
			this.location = location;
			this.description = description;
			this.images = images;
			if (images != null && images.Count > 0) { mainImage = images.First(); }
			else mainImage = "\\constants\\logo2.jpg";
		}

		public string Description { get => description; set => description = value; }
		public List<string> Images { get => images; set => images = value; }
		public Location Location { get => location; set => location = value; }
		public string Name { get => name; set => name = value; }
		public string MainImage { get => mainImage; set => mainImage = value; }
	}
}
