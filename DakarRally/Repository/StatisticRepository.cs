using DakarRally.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static DakarRally.DbModel.EnumTypes;

namespace DakarRally.Repository
{
    public class StatisticRepository
    {
        ApiContext _context;
        public StatisticRepository(ApiContext context)
        {
            _context = context;
        }
        public void SaveRaceVehicleStatistic(RaceVehicleStatistic raceVehicleStatistic)
        {
            if (raceVehicleStatistic?.Id == 0)
            {
                _context.RaceVehicleStatistic.Add(raceVehicleStatistic);
            };
            _context.SaveChanges();
        }

        public IEnumerable<RaceVehicleStatistic> Get()
        {
            return _context.RaceVehicleStatistic.ToList();
        }

        public IEnumerable<object> GetLeaderBoard(string vehicleType = null)
        {
            var statistic = _context.RaceVehicleStatistic
                   .Include(x => x.Vehicle)
                   .Include(x => x.Race)
           .Where(x => vehicleType == null || x.Vehicle.VehicleType.ToString() == vehicleType);

            var result = (from s in statistic
                          select new
                          {
                              Race = s.Race.Year,
                              Team = s.Vehicle.TeamName,
                              VehicleType = s.Vehicle.VehicleType.ToString(),
                              VehicleModel = s.Vehicle.VehicleModel.ToString(),
                              Distance = s.Distance,
                              FinshTime = s.FinishTimeHours,
                              IsFinishRace = s.isFinish
                          }).OrderBy(x => x.IsFinishRace).OrderByDescending(x => x.FinshTime);

            return result;
        }

        public object GetRaceStatus(int raceId)
        {
            var statistic = _context.RaceVehicleStatistic
                   .Include(x => x.Vehicle)
                   .Include(x => x.Race).Where(x => x.RaceId == raceId);

            var isPending = statistic.All(x => x.FinishTimeHours == 0);
            var isRunning = statistic.Any(x => !x.isHeavyMalFunction && x.Distance < Race.Distance);

            var status = ((RaceStatusEnum)statistic.FirstOrDefault().Race.RaceStatusId).ToString();

            var vehiclesFinish = statistic.Where(x => x.isFinish).Count();
            var vehiclesHeavyMalfunction = statistic.Where(x => x.isHeavyMalFunction).Count();
            var cars = statistic.Count(x => x.Vehicle.VehicleType == VehicleTypeEnum.Car);
            var motor= statistic.Count(x => x.Vehicle.VehicleType == VehicleTypeEnum.Motorcycle);
            var truck = statistic.Count(x => x.Vehicle.VehicleType == VehicleTypeEnum.Truck);

            var result = new
            {
                status = status,
                finishTotal = vehiclesFinish,
                malfunctionTotal = vehiclesHeavyMalfunction,
                carTotal = cars,
                motorTotal = motor,
                truckTotal = truck
            };

            return result;
        
        }

        public IEnumerable<object> VehicleStatistic(int vehicleId)
        {
            var statistic = _context.RaceVehicleStatistic
                .Include(x => x.Vehicle)
                .Include(x => x.Race).Where(x => x.Vehicle.Id == vehicleId).ToList();

            var result = from s in statistic
                         select new
                         {
                             Race = s.Race.Year,
                             Team = s.Vehicle.TeamName,
                             VehicleModel = s.Vehicle.VehicleModel.ToString(),
                             VehicleType = s.Vehicle.VehicleType.ToString(),
                             Distance = s.Distance,
                             MalfunctionTime = s.Vehicle.GetRepairmentTime() * s.MalfunctionNo,
                             IsFinishRace = s.isFinish,
                             FinishTime = s.FinishTimeHours
                         };

            return result;
        }

        public IEnumerable<object> FindVehicle(string team = null
                                                                        , string model = null
                                                                        , DateTime? date = null
                                                                        , bool? isFinish = null
                                                                        , double? distance = 0)
        {
            var statistic = _context.RaceVehicleStatistic
                .Include(x => x.Vehicle)
                .Include(x => x.Race) .Where(x => (team == null || x.Vehicle.TeamName == team)
            && (model == null || x.Vehicle.VehicleModel.ToString() ==  model)
            && (date == null || x.Vehicle.ManifacturingDate.Date == date.Value.Date)
            && (isFinish == null || x.isFinish == isFinish)
            && (distance == null || x.Distance == distance)).ToList();

            var result = from s in statistic
                         select new
                         {
                             Race = s.Race.Year,
                             Team = s.Vehicle.TeamName,
                             VehicleModel = s.Vehicle.VehicleModel.ToString(),
                             VehicleType = s.Vehicle.VehicleType.ToString(),
                             ManufacturDate = s.Vehicle.ManifacturingDate,
                             Distance = s.Distance,
                             IsFinishRace = s.isFinish,
                             FinshTime = s.FinishTimeHours
                         };

            return result;

        }

    }
}
