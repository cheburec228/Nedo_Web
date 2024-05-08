using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_01.Controllers
{
    public class CController : Controller
    {
        public ActionResult View3()
        {
            ViewBag.Side3 = TempData["Side3"];
            return View();
        }

        [HttpPost]
        public ActionResult View3(double side3)
        {
            TempData["Side3"] = side3;
            return RedirectToAction("View4", "D");
        }
    }
}