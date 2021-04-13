using DakarRally.DbModel;
using DakarRally.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleRepository _vehicleRepository;

        public VehiclesController(ApiContext context)
        {
            _vehicleRepository = new VehicleRepository(context);
        }

        // PUT: api/Vehicles
        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle vehicle)
        {
            var result = _vehicleRepository.Save(vehicle);
            return result;
        }
    }
}
