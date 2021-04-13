using System.Collections.Generic;
using System.Linq;

namespace DakarRally.DbModel
{
    public class Race
    {

        public const int Distance = 1000;
        public int Id { get; set; }
        public int Year { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public int RaceStatusId { get; set; }
    }
    
}
