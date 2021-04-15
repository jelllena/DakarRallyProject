using DakarRally.DbModel;
using DakarRally.Repository;
using DakarRally.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DakarRally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly RaceRepository _raceRepository;
        private readonly RaceService _raceService;

        public RacesController(ApiContext context)
        {
            _raceRepository = new RaceRepository(context);
            _raceService = new RaceService(context);
        }

        // POST: api/Races
        [HttpPost]
        [Route("{year}")]
        public ActionResult<Race> PostRace(int year)
        {
            var race = _raceRepository.AddRace(year);
            return CreatedAtAction("GetRace", new { id = race.Id }, race);
        }

        // GET: api/Races/1/run
        [HttpPost]
        [Route("{id}/run")]
        public ActionResult<string> PostRunRace(int id)
        {
             _raceService.RunRace(id);
            return "FINISHED!";
        }

        // PUT api/Races/5/Vehicle/
        [HttpPut]
        [Route("{id}/vehicle")]
        public ActionResult<Race> PutVehicle(int id, [FromBody] Vehicle vehicle)
        {
            _raceRepository.PutVehicle(id, vehicle);
            return CreatedAtAction("GetRace", new { id = id });
        }

        // DELETE: api/Races/Vehicle/3
        [HttpDelete]
        [Route("vehicle/{id}")]
        public ActionResult<bool> DeleteRaceVehicle(int id)
        {
            _raceRepository.DeleteVehicle(id);
            return true;
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        public ActionResult<Race> GetRace(int id)
        {
            var race = _raceRepository.GetRace(id);
            if (race == null)
            {
                return NotFound($"Race id {id}");
            }
            
            return race;
        }

        // GET: api/Races/
        [HttpGet]
        public ActionResult<IEnumerable<Race>> GetRace()
        {
            var result = _raceRepository.GetRace();
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // POST: api/Races
        [HttpPost]
        public ActionResult<Race> PostRace(Race race)
        {
            _raceRepository.Save(race);
            return CreatedAtAction("GetRace", new { id = race.Id }, race);
        }


        [Route("index")]
        public string Get()
        {
            return "Dakar Rally API \n\n"

            + "1: POST api/races/{year} \n"
            + "2. PUT  api/races/{id}/vehicle \n"
            + "3. PUT: api/Vehicles \n"
            + "4. DELETE api/races/vehicle/{id} \n"
            + "5. POST api/races/{id}/run \n"
            + "6. GET api/statistic/leaderboard \n"
            + "7. GET api/statistic/leaderboard/type \n"
            + "8. GET api/statistic/vehicles/{id} \n"
            + "9. GET api/statistic/vehicles/filter/?team={}?model={}?date={}?isFinish=?distance={} \n"
            + "10. GET api/statistic/races/{id} \n";
        }
    }
}

       
