using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class BoatLocations
    {
        public BoatLocations()
        {
            
        }
        public string[] AircraftCarrier { get; set; } = new string[5];
        public string[] Destroyer { get; set; } = new string[4];
        public string[] Warship { get; set; } = new string[3];
        public string[] Submarine { get; set; } = new string[2];

        public bool isEmpty(string[] list)
        {
            bool f = true;
            foreach (string s in list)
            {
                if (s != null) f = false;
            }

            return f;
        }

        public bool CheckDestroyer()
        {
            
            if (isEmpty(Destroyer)) return true;
            var list = this.AircraftCarrier.Intersect(Destroyer);
            return list.Any();
        }

        public bool CheckWarship()
        {
            if (isEmpty(Warship)) return true;
            var list = Destroyer.Concat(AircraftCarrier);
            list = this.Warship.Intersect(list);
            return list.Any();
        }

        public bool CheckSub()
        {
            if (isEmpty(Submarine)) return true;
            var list = Destroyer.Concat(AircraftCarrier).Concat(Warship);
            list = this.Submarine.Intersect(list);
            return list.Any();
        }

        public override string ToString()
        {
            return "{" + AircraftCarrier[0] + "," + AircraftCarrier[1] + "," + AircraftCarrier[2] + "," +
                   AircraftCarrier[3] + "," + AircraftCarrier[4] + "}" +
                   "{" + Destroyer[0] + "," + Destroyer[1] + "," + Destroyer[2] + "," + Destroyer[3] + "}" +
                   "{" + Warship[0] + "," + Warship[1] + "," + Warship[2] + "}" +
                   "{" + Submarine[0] + "," + Submarine[1] + "}";

        }
    }

}
