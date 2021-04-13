using DakarRally.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static DakarRally.DbModel.EnumTypes;

namespace DakarRally.Repository
{
    public class VehicleRepository
    {
        ApiContext _context;
        public VehicleRepository(ApiContext context)
        {
            _context = context;
        }

        public Vehicle Save(Vehicle vehicle)
        {
            if (!VehicleExists(vehicle.Id))
            {
                _context.Add(vehicle);
            }
            _context.SaveChanges();

            return vehicle;
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }

    }
}

