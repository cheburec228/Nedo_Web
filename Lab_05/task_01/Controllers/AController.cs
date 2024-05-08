using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_01.Controllers
{
    public class AController : Controller
    {
        // GET: A
        public ActionResult View1()
        {
            ViewBag.Side1 = TempData["Side1"];
            return View();
        }

        [HttpPost]
        public ActionResult View1(double side1)
        {
            TempData["Side1"] = side1;
            return RedirectToAction("View2", "B");
        }

    }
}
