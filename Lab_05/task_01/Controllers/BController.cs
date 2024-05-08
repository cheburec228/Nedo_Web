using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_01.Controllers
{
    public class BController : Controller
    {
        public ActionResult View2()
        {
            ViewBag.Side2 = TempData["Side2"];
            return View();
        }

        [HttpPost]
        public ActionResult View2(double side2)
        {
            TempData["Side2"] = side2;
            return RedirectToAction("View3", "C");
        }
    }
}