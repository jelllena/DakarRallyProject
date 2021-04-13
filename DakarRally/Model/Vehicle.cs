using System;
using static DakarRally.DbModel.EnumTypes;

namespace DakarRally.DbModel
{
    public class Vehicle
    {
        public  int Id { get; set; }
        public string TeamName { get; set; }

        public VehicleModelEnum VehicleModel { get; set; }

        //car, sports, truck;
        public VehicleTypeEnum VehicleType { get; set; }
        public DateTime ManifacturingDate { get; set; }
        public double Velocity { get; set; }


        public bool CheckLightMalFunction()
        {
            Random r = new Random();

            if (this.VehicleModel == VehicleModelEnum.Sport && this.VehicleType == VehicleTypeEnum.Car)
            { 
                if (r.Next(0, 100) <= 12)
                {
                    return true;
                }
            }

            if(this.VehicleModel == VehicleModelEnum.Terran && this.VehicleType == VehicleTypeEnum.Car)
            {
                if (r.Next(0, 100) <= 3)
                {
                    return true;
                }
            }

            if (this.VehicleType == VehicleTypeEnum.Truck)
            {
                if (r.Next(0, 100) <= 6)
                {
                    return true;
                }
            }

            if (this.VehicleModel == VehicleModelEnum.Cross && this.VehicleType == VehicleTypeEnum.Motorcycle)
            {
                if (r.Next(0, 100) <= 3)
                {
                    return true;
                }
            }

            if (this.VehicleModel == VehicleModelEnum.Sport && this.VehicleType == VehicleTypeEnum.Motorcycle)
            {
                if (r.Next(0, 100) <= 18)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckHeavyMalfunction()
        {
            Random r = new Random();

            if (this.VehicleModel == VehicleModelEnum.Sport && this.VehicleType == VehicleTypeEnum.Car)
            {
                if (r.Next(0, 100) <= 2)
                {
                    return true;
                }
            }

            if (this.VehicleModel == VehicleModelEnum.Terran && this.VehicleType == VehicleTypeEnum.Car)
            {
                if (r.Next(0, 100) <= 1)
                {
                    return true;
                }
            }

            if (this.VehicleType == VehicleTypeEnum.Truck)
            {
                if (r.Next(0, 100) <= 4)
                {
                    return true;
                }
            }

            if (this.VehicleModel == VehicleModelEnum.Cross && this.VehicleType == VehicleTypeEnum.Motorcycle)
            {
                if (r.Next(0, 100) <= 2)
                {
                    return true;
                }
            }

            if (this.VehicleModel == VehicleModelEnum.Sport && this.VehicleType == VehicleTypeEnum.Motorcycle)
            {
                if (r.Next(0, 100) <= 10)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetRepairmentTime()
        {
            if (this.VehicleType == VehicleTypeEnum.Car)
            {
                return 5;
            }

            if (this.VehicleType == VehicleTypeEnum.Truck)
            {
                return 7;
            }

            if (this.VehicleType == VehicleTypeEnum.Motorcycle)
            {
                return 3;
            }

            return 0;
        }
    }
}
