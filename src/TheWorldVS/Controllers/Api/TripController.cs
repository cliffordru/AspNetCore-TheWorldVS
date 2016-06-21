using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorldVS.Models;
using TheWorldVS.ViewModels;

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
        public JsonResult Post([FromBody]TripViewModel newTrip)
        {
            if(ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Created;
                return Json(true);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
