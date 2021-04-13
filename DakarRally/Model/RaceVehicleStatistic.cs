using System.Collections.Generic;

namespace DakarRally.DbModel
{
    public class RaceVehicleStatistic
    {
        public int Id { get; set; }
        public int RaceId { get; set; }

        public int VehicleId { get; set; }
        public double Distance { get; set; }
        public int FinishTimeHours { get; set; }
        public int MailfunctionTimeHours { get; set; }

        public int MalfunctionNo { get; set; }
        public int MalfunctionRestHours { get; set; }

        public bool isHeavyMalFunction {get; set;}

        public bool isFinish { get; set; }

        public Vehicle Vehicle { get; set; }

        public Race Race { get; set; }

    }
}
