using System.Runtime.Serialization;

namespace DakarRally.DbModel
{
    public class EnumTypes
    {
        
        
        public enum VehicleTypeEnum: short
        {
            [EnumMember(Value = "Car")]
            Car = 1,
            [EnumMember(Value = "Truck")]
            Truck = 2,
            [EnumMember(Value = "Motorcycle")]
            Motorcycle = 3
        }


        public enum VehicleModelEnum : short
        {
            [EnumMember(Value = "")]
            Truck = 0,
            [EnumMember(Value = "Terran")]
            Terran = 1,
            [EnumMember(Value = "Sport")]
            Sport = 2,
            [EnumMember(Value = "Cross")]
            Cross = 3
        }

        public enum RaceStatusEnum : short
        { 
            Pending = 0,
            Run = 1,
            Finish = 2
        }
    }
}
