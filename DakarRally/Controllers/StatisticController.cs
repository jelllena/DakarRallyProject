using DakarRally.DbModel;
using DakarRally.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DakarRally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly StatisticRepository _statisticRepository;
        public StatisticController(ApiContext context)
        {
            _statisticRepository = new StatisticRepository(context);
        }

        [HttpGet("{type?}")]
        [Route("leaderboard/{type?}")]
        public string GetLeaderBoard(string type)
        {
            var result = _statisticRepository.GetLeaderBoard(type);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        [Route("vehicles/{id}")]
        public string GetVehicleStatistic(int id)
        {
            var result = _statisticRepository.VehicleStatistic(id);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        //[Route("vehicles/filter/{team?}/{model?}/{date?}/{isFinish?}/{distance?}")]
        [Route("vehicles/filter")]
        public string FindVehicle(string team, string model, DateTime? date, bool? isFinish, double? distance)
        {
            var result = _statisticRepository.FindVehicle(team, model, date, isFinish, distance);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        [Route("statistic/races/{id}")]
        public string GetRaceStatus(int id)
        {
            var result = _statisticRepository.GetRaceStatus(id);
            return JsonConvert.SerializeObject(result);
        }

        public ActionResult<IEnumerable<RaceVehicleStatistic>> Get()
        {
            var result = _statisticRepository.Get().ToList();
            return result;
        }
    }
}
