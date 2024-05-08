using System.Linq;
using System.Web.Mvc;
using task_07.Models;

namespace task_07.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new RepeatInput());
        }

        [HttpPost]
        public ActionResult Index(RepeatInput model)
        {
            if (ModelState.IsValid)
            {
                // Перевірка, чи введено не менше 5 слів
                string[] words = model.InputText.Split(' ');
                if (words.Length < 5)
                {
                    ModelState.AddModelError("InputText", "Рядок повинен містити не менше п'яти слів.");
                    return View(model);
                }

                // Обробка введеного рядка
                string lastWord = words.LastOrDefault();
                string processedText = "";
                foreach (string word in words)
                {
                    if (word == lastWord)
                        processedText += "<strong>" + word + "</strong> ";
                    else
                        processedText += word + " ";
                }

                // Повторення обробленого рядка задану кількість разів з переносом рядків
                model.ProcessedText = string.Join("<br/>", Enumerable.Repeat(processedText, model.RepeatCount));

                return View(model);
            }

            // Якщо дані невірні, повернути форму з помилками
            return View(model);
        }
    }
}
