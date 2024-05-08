using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_05.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalculateVolume(int radius)
        {
            double volume = (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3);

            return Json(new { result = volume });
        }

        [HttpPost]
        public ActionResult CalculateSurfaceArea(int radius)
        {
            double surfaceArea = 4 * Math.PI * Math.Pow(radius, 2);

            return Json(new { result = surfaceArea });
        }
    }

}