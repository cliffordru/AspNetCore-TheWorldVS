using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorldVS.Controllers.Web
{
    public class AppController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
