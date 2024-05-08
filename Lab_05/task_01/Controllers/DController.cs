using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_01.Controllers
{
    public class DController : Controller
    {
        public ActionResult View4()
        {
            double side1 = (double)TempData["Side1"];
            double side2 = (double)TempData["Side2"];
            double side3 = (double)TempData["Side3"];

            double perimeter = side1 + side2 + side3;
            ViewBag.Side1 = side1;
            ViewBag.Side2 = side2;
            ViewBag.Side3 = side3;

            double longestSide = Math.Max(side1, Math.Max(side2, side3));
            ViewBag.LongestSide = longestSide;

            ViewBag.Perimeter = perimeter;

            return View();
        }
    }
}