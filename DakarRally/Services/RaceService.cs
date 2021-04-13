using DakarRally.DbModel;
using DakarRally.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DakarRally.Services
{
    public class RaceService
    {
        ApiContext _context;
        RaceRepository _raceRepository;
        StatisticRepository _statisticRepository;

        public RaceService(ApiContext context)
        {
            _context = context;
            _raceRepository = new RaceRepository(context);
            _statisticRepository = new StatisticRepository(context);
        }

        public void RunRace(int raceId)
        {
            var race = _context.Race.Include(x => x.Vehicles).FirstOrDefault(x => x.Id == raceId);

            do
            {
                foreach (var vehicle in race.Vehicles)
                {
                    var raceVehicleStatistic = _context.RaceVehicleStatistic
                        .Include(x => x.Race)
                        .Include(x => x.Vehicle)
                        .FirstOrDefault(x => x.Race.Id == race.Id && x.Vehicle.Id == vehicle.Id);

                    if (raceVehicleStatistic == null)
                    {
                        raceVehicleStatistic = new RaceVehicleStatistic();
                        raceVehicleStatistic.RaceId = race.Id;
                        raceVehicleStatistic.VehicleId = vehicle.Id;

                        //run
                        race.RaceStatusId = 1;
                    }

                    // skip vehicle ( if it is finished or has had heavy malfunction)
                    if (raceVehicleStatistic.isFinish || raceVehicleStatistic.isHeavyMalFunction)
                    {
                        continue;
                    }

                    //go next hour
                    raceVehicleStatistic.FinishTimeHours = raceVehicleStatistic.FinishTimeHours + 1;

                    //light malfunction pending...continue
                    var hasLightMalfunction = raceVehicleStatistic.MalfunctionRestHours > 0;
                    if (hasLightMalfunction)
                    {
                        raceVehicleStatistic.MalfunctionRestHours = raceVehicleStatistic.MalfunctionRestHours - 1;
                        _statisticRepository.SaveRaceVehicleStatistic(raceVehicleStatistic);
                        
                        continue;
                    }

                    //add distance
                    var newDistance = raceVehicleStatistic.Distance + vehicle.Velocity;
                    var isFinish = newDistance >= Race.Distance;
                    raceVehicleStatistic.Distance = isFinish ? Race.Distance : newDistance; 
                    raceVehicleStatistic.isFinish = isFinish;
                    _statisticRepository.SaveRaceVehicleStatistic(raceVehicleStatistic);

                    if (isFinish)
                    {
                        continue;
                    }

                    //check heavy
                    var checkHeavy = vehicle.CheckHeavyMalfunction();
                    if (checkHeavy)
                    {
                        raceVehicleStatistic.isHeavyMalFunction = true;
                        _statisticRepository.SaveRaceVehicleStatistic(raceVehicleStatistic);
                        continue;
                    }

                    var checkLightMalfunction = vehicle.CheckLightMalFunction();
                    if (checkLightMalfunction)
                    {
                        raceVehicleStatistic.MalfunctionNo = raceVehicleStatistic.MalfunctionNo + 1;
                        raceVehicleStatistic.MalfunctionRestHours = vehicle.GetRepairmentTime();
                        _statisticRepository.SaveRaceVehicleStatistic(raceVehicleStatistic);
                        continue;
                    }
                }
            }
            while (_raceRepository.IsRaceFinished(race.Id) == false);

            //race finished
            race.RaceStatusId = 2;
            _context.SaveChanges();
            
        }
    }
}
