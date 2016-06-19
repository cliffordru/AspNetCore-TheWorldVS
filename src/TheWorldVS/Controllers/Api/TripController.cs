using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorldVS.Models;

namespace TheWorldVS.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private IWorldRepository _respository;

        public TripController(IWorldRepository repository)
        {
            _respository = repository;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = _respository.GetAllTripsWithStops();
            return Json(trips);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]Trip newTrip)
        {
            return Json(true);
        }
    }
}
