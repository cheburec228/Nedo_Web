using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_01.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class TestController : Controller
    {
        public ActionResult Index()
        {
            // Создаем список вопросов для теста
            var testQuestions = GenerateTestQuestions();

            return View(testQuestions);
        }

        [HttpPost]
        public ActionResult SubmitTest(List<TestQuestion> testQuestions)
        {
            // Здесь вы можете обработать результаты теста и сохранить их в базу данных
            // В этом примере просто выводим результаты в консоль
            foreach (var question in testQuestions)
            {
                if (question.Type == QuestionType.Text)
                {
                    // Обработка ответа в текстовом поле
                    string answer = Request.Form["question-" + testQuestions.IndexOf(question)];
                    System.Console.WriteLine($"Ответ на вопрос \"{question.Text}\": {answer}");
                }
                else if (question.Type == QuestionType.SingleChoice)
                {
                    // Обработка ответа с радио кнопками
                    string answer = Request.Form["question-" + testQuestions.IndexOf(question)];
                    System.Console.WriteLine($"Ответ на вопрос \"{question.Text}\": {answer}");
                }
                else if (question.Type == QuestionType.MultipleChoice)
                {
                    // Обработка ответа с выбором нескольких вариантов
                    var selectedOptions = Request.Form.GetValues("question-" + testQuestions.IndexOf(question) + "[]");
                    if (selectedOptions != null && selectedOptions.Length > 0)
                    {
                        string answer = string.Join(", ", selectedOptions);
                        System.Console.WriteLine($"Ответ на вопрос \"{question.Text}\": {answer}");
                    }
                }
            }

            // Здесь можно выполнить дополнительные действия, например, перенаправить пользователя на другую страницу
            return RedirectToAction("Index");
        }

        private List<TestQuestion> GenerateTestQuestions()
        {
            // Генерация тестовых вопросов
            var questions = new List<TestQuestion>();

            // Создаем 10 тестовых вопросов
            for (int i = 1; i <= 10; i++)
            {
                var question = new TestQuestion
                {
                    Text = $"Вопрос {i}",
                    Type = (QuestionType)(i % 3) // Просто чередуем типы вопросов для примера
                };

                // Добавляем варианты ответов для вопросов с выбором варианта
                if (question.Type == QuestionType.SingleChoice || question.Type == QuestionType.MultipleChoice)
                {
                    question.Options = new List<Option>
                {
                    new Option { Text = $"Вариант ответа 1 для вопроса {i}" },
                    new Option { Text = $"Вариант ответа 2 для вопроса {i}" },
                    new Option { Text = $"Вариант ответа 3 для вопроса {i}" }
                };
                }

                questions.Add(question);
            }

            return questions;
        }
    }

}