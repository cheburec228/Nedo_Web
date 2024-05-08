using System.Web.Mvc;

namespace Lab_02.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelHome formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }


            return View(formData);
        }
    }
}
