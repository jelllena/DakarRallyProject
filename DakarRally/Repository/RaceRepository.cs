using DakarRally.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Repository
{
    public class RaceRepository
    {
        ApiContext _context;
        public RaceRepository(ApiContext context)
        {
            _context = context;
        }

        public Race Save(Race race)
        {
            if (!RaceExists(race.Id))
            {
                _context.Add(race);
            }

            _context.SaveChanges();

            return race;

        }

        public Race GetRace(int id)
        {
            var result = _context.Race.Include(v => v.Vehicles).FirstOrDefault(x => x.Id == id);
            return result;
        }

        public Race AddRace(int year)
        {
            Race race = new Race { Year = year };
            _context.Race.Add(race);
            _context.SaveChanges();

            return race;
        }

        public List<Race> GetRace()
        {
            var result = _context.Race.Include(v => v.Vehicles).ToList();
            return result;
        }
        public bool IsRaceFinished(int raceId)
        {
            var vehicleStatistic = _context.RaceVehicleStatistic.Where(x => x.RaceId == raceId);
            if (!vehicleStatistic.Any() || vehicleStatistic.Any(x => x.isHeavyMalFunction == false && x.Distance < Race.Distance))
            {
                return false;
            }

            return true;
        }

        private bool RaceExists(int id)
        {
            return _context.Race.Any(e => e.Id == id);
        }

        public void PutVehicle(int raceId, Vehicle vehicle)
        {
            var race = GetRace(raceId);
            if (race == null) return;

            if (!race.Vehicles.Any())
            {
                race.Vehicles = new List<Vehicle> { };
            }

            var vehicleExists = race.Vehicles.Any(x => x.Id == vehicle.Id);
            if (vehicleExists) return;


            race.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void DeleteVehicle(int vehicleId)
        {
            var race = _context.Race.Include(v => v.Vehicles).Where(x => x.Vehicles.Any(x => x.Id == vehicleId)).FirstOrDefault();
            if (race != null)
            {
                var vehicleToDelete = race.Vehicles.FirstOrDefault(x => x.Id == vehicleId);
                if (vehicleToDelete != null)
                {
                    race.Vehicles.Remove(vehicleToDelete);
                    _context.SaveChanges();
                }
            }
        }
    }
}
