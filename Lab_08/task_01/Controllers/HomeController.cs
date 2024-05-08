using System.Web.Mvc;

namespace task_01.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SuccessAction()
        {
            // Действия при успешном логине
            return View();
        }
    }
}
