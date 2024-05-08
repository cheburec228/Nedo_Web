using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using task_02_2.Models;

namespace task_02_2.Controllers
{
    public class HomeController : Controller
    {
        private Lab_07_DbContext db = new Lab_07_DbContext();

        // GET: Home
        public ActionResult Index()
        {
            var dataList = db.Details.ToList();
            return View(dataList);
        }

        [HttpPost]
        public ActionResult AddDetail(Detail detail)
        {
            if (ModelState.IsValid)
            {
                db.Details.Add(detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Если модель недопустима, возвращаем представление с сообщениями об ошибках
            var dataList = db.Details.ToList();
            return View("Index", dataList);
        }

        [HttpPost]
        public ActionResult ClearTable()
        {
            foreach (var detail in db.Details)
            {
                db.Details.Remove(detail);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteDetail(int id)
        {
            var detail = db.Details.Find(id);
            if (detail != null)
            {
                db.Details.Remove(detail);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveDetail(int id, string name, string type, int quantity, int weight)
        {
            // Валидация данных перед сохранением изменений
            if (!IsValidName(name))
            {
                ModelState.AddModelError("name", "Невірний формат для найменування.");
                return RedirectToAction("Index");
            }

            if (!IsValidType(type))
            {
                ModelState.AddModelError("type", "Тип повинен бути 'З', 'П' або 'О'.");
                return RedirectToAction("Index");
            }

            if (!IsValidNumber(quantity))
            {
                ModelState.AddModelError("quantity", "Кількість повинна бути цілим числом.");
                return RedirectToAction("Index");
            }

            if (!IsValidNumber(weight))
            {
                ModelState.AddModelError("weight", "Вага повинна бути цілим числом.");
                return RedirectToAction("Index");
            }

            var detail = db.Details.Find(id);
            if (detail != null)
            {
                detail.Name = name;
                detail.Type = type;
                detail.Quantity = quantity;
                detail.Weight = weight;

                db.Entry(detail).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private bool IsValidName(string name)
        {
            // Допустимы любые значения для названия
            return true;
        }

        private bool IsValidType(string type)
        {
            // Тип должен быть 'З', 'П' или 'О'
            return Regex.IsMatch(type, "^[ЗПО]$");
        }

        private bool IsValidNumber(int number)
        {
            // Проверка, что число является целым числом
            return true;
        }
    }
}
