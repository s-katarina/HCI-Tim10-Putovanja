using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.User
{
    public class Record
    {
        private AppUser user;
        public AppUser User { get => user; set => user = value; }

        private Trip trip;
        public Trip Trip { get => trip; set => trip = value; }

        private DateTime date;
        public DateTime Date { get => date; set => date = value; }

        public Record(AppUser user, Trip trip, DateTime date)
        {
            this.user = user;
            this.trip = trip;
            this.date = date;
        }
    }
}
