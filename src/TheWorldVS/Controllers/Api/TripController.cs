using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorldVS.Controllers.Api
{
    public class TripController : Controller
    {
        [HttpGet("api/trips")]
        public JsonResult Get()
        {
            return Json(new { name = "Cliff" });
        }
    }
}
