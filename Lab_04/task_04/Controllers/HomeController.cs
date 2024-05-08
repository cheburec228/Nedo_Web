using System.Web.Mvc;
using task_04.Models;

namespace task_04.Controllers
{
    public class HomeController : Controller
    {
        private static int visitCount = 0;

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormData formData)
        {
            visitCount++;
            ViewBag.VisitCount = visitCount;

            if (ModelState.IsValid)
            {
                // Здесь можно добавить обработку данных, если это необходимо
                return View("Index", formData); // Отобразить данные на той же странице
            }
            else
            {
                // Возвращаем ту же страницу с данными формы для исправления ошибок
                return View(formData);
            }
        }
    }
}
