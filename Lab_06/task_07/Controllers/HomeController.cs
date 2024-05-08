using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using task_07.Models;

namespace task_07.Controllers
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
        public ActionResult AddDetail(string name, string type, int quantity, int weight)
        {
            var detail = new Detail
            {
                Name = name,
                Type = type,
                Quantity = quantity,
                Weight = weight
            };

            db.Details.Add(detail);
            db.SaveChanges();

            return RedirectToAction("Index");
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
    }
}
