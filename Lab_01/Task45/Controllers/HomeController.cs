using System.Web.Mvc;

namespace Task45.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int counter = (Session["CounterIndex"] != null) ? (int)Session["CounterIndex"] : 0;
            counter++;
            Session["CounterIndex"] = counter;
            ViewBag.Counter = counter;
            return View();
        }

        public ActionResult A()
        {
            UpdateCounter("CounterA");
            ViewBag.Message = "a.";
            return View();
        }

        public ActionResult B()
        {
            UpdateCounter("CounterB");
            ViewBag.Message = "b.";
            return View();
        }

        public ActionResult C()
        {
            UpdateCounter("CounterC");
            ViewBag.Message = "c.";
            return View();
        }

        public ActionResult D()
        {
            UpdateCounter("CounterD");
            ViewBag.Message = "d.";
            return View();
        }

        private void UpdateCounter(string counterKey)
        {
            int counter = (Session[counterKey] != null) ? (int)Session[counterKey] : 0;
            counter++;
            Session[counterKey] = counter;
            ViewBag.Counter = counter;
        }
    }
}
