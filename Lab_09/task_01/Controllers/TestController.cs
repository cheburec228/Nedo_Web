using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace task_01.Controllers
{
    public class TestController : Controller
    {
        private string connectionString = "server=localhost;user=root;password=0672951355;database=loginsdb";

        public ActionResult Index()
        {
            // Получение данных текущего пользователя
            var currentUserEmail = Session["CurrentUserEmail"] as string;

            // Проверка количества попыток для текущего пользователя
            ViewBag.AttemptCount = GetAttemptNumber(currentUserEmail);

            var questions = GetQuestions();
            return View(questions);
        }

        [HttpPost]
        public ActionResult SubmitTest(FormCollection form)
        {
            var correctCount = CheckAnswers(form);
            ViewBag.CorrectCount = correctCount;

            // Получение данных текущего пользователя
            var currentUserEmail = Session["CurrentUserEmail"] as string;

            // Получение номера попытки
            int attemptNumber = GetAttemptNumber(currentUserEmail) + 1;

            // Сохранение результатов теста в базе данных MySQL
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO TestResults (Email, AttemptNumber, CorrectAnswersCount) VALUES (@Email, @AttemptNumber, @CorrectAnswersCount)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", currentUserEmail);
                command.Parameters.AddWithValue("@AttemptNumber", attemptNumber);
                command.Parameters.AddWithValue("@CorrectAnswersCount", correctCount);
                command.ExecuteNonQuery();
            }

            return View("SubmitTest");
        }

        private int GetAttemptNumber(string email)
        {
            int attemptNumber = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MAX(AttemptNumber) FROM TestResults WHERE Email = @Email";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    attemptNumber = Convert.ToInt32(result);
                }
            }

            return attemptNumber;
        }

        private int CheckAnswers(FormCollection form)
        {
            // Получаем правильные ответы
            var correctAnswers = new Dictionary<string, string>
            {
                { "1. Яка формула відстані між двома точками в тривимірному просторі?", "a) sqrt((x2 - x1)^2 + (y2 - y1)^2 + (z2 - z1)^2)" },
                { "2. Як виразити вектор в просторі в координатах?", "b) За напрямними косинусами" },
                { "3. Які з наведених рівнянь задають площину?", "c) x + 2z = 7" },
                { "4. Як виразити кут між двома векторами?", "a) Через скалярний добуток" },
                { "5. Які з перелічених фігур є плоскими?", "d) Паралелограм" },
                { "6. Які з наступних рівнянь задають пряму?", "a) y = 2x + 3" },
                { "7. Які з перелічених точок лежать на площині z = 2x + y?", "a) (1, 2, 4)" },
                { "8. Як називається точка, в якій дотична до графіку функції збігається з віссю абсцис?", "Децимальна" },
                { "9. Знайдіть точку перетину прямої та площини: 2x - y + 3z = 6 і x + y - z = 1", "Точка (1, 2, 1)" },
                { "10. Які координати центру та радіуса кола, заданого рівнянням x^2 + y^2 - 6x + 8y - 5 = 0?", "Центр (3, -4), радіус 1" }
            };

            // Получаем ответы пользователя из формы
            var userAnswers = new Dictionary<string, string>();
            foreach (var key in form.AllKeys)
            {
                userAnswers.Add(key, form[key]);
            }

            // Подсчет количества правильных ответов
            var correctCount = 0;
            foreach (var kvp in correctAnswers)
            {
                if (userAnswers.ContainsKey(kvp.Key) && userAnswers[kvp.Key] == kvp.Value)
                {
                    correctCount++;
                }
            }

            return correctCount;
        }

        private List<QuestionViewModel> GetQuestions()
        {
            var questions = new List<QuestionViewModel>
            {
                // Вопросы з радіокнопками
                new QuestionViewModel
                {
                    Question = "1. Яка формула відстані між двома точками в тривимірному просторі?",
                    Type = QuestionType.SingleChoice,
                    Options = new List<string>
                    {
                        "a) sqrt((x2 - x1)^2 + (y2 - y1)^2 + (z2 - z1)^2)",
                        "b) sqrt((x2 - x1)^2 + (y2 - y1)^2)",
                        "c) (x2 - x1) + (y2 - y1) + (z2 - z1)",
                        "d) sqrt((x2 - x1)^2 + (y2 - y1)^2 - (z2 - z1)^2)"
                    }
                },
                new QuestionViewModel
                {
                    Question = "2. Як виразити вектор в просторі в координатах?",
                    Type = QuestionType.SingleChoice,
                    Options = new List<string>
                    {
                        "a) Координатами початку та кінця",
                        "b) За напрямними косинусами",
                        "c) За нормаллю",
                        "d) За радіусом"
                    }
                },
                new QuestionViewModel
                {
                    Question = "3. Які з наведених рівнянь задають площину?",
                    Type = QuestionType.SingleChoice,
                    Options = new List<string>
                    {
                        "a) 2x + 3y = 5",
                        "b) y = x^2",
                        "c) x + 2z = 7",
                        "d) 3y - 2z = 4"
                    }
                },
                new QuestionViewModel
                {
                    Question = "4. Як виразити кут між двома векторами?",
                    Type = QuestionType.SingleChoice,
                    Options = new List<string>
                    {
                        "a) Через скалярний добуток",
                        "b) Через векторний добуток",
                        "c) Через детермінант матриці",
                        "d) Через інтеграл"
                    }
                },
                // Вопросы з чекбоксами
                new QuestionViewModel
                {
                    Question = "5. Які з перелічених фігур є плоскими?",
                    Type = QuestionType.MultipleChoice,
                    Options = new List<string>
                    {
                        "a) Куб",
                        "b) Конус",
                        "c) Тетраедр",
                        "d) Паралелограм",
                        "e) Квадрат",
                        "f) Окружність"
                    }
                },
                new QuestionViewModel
                {
                    Question = "6. Які з наступних рівнянь задають пряму?",
                    Type = QuestionType.MultipleChoice,
                    Options = new List<string>
                    {
                        "a) y = 2x + 3",
                        "b) x^2 + y^2 = 25",
                        "c) 3x + 4y = 12",
                        "d) x^2 + y^2 + z^2 = 1"
                    }
                },
                new QuestionViewModel
                {
                    Question = "7. Які з перелічених точок лежать на площині z = 2x + y?",
                    Type = QuestionType.MultipleChoice,
                    Options = new List<string>
                    {
                        "a) (1, 2, 4)",
                        "b) (0, 0, 0)",
                        "c) (1, 0, 2)",
                        "d) (2, 0, 3)"
                    }
                },
                // Вопросы з відкритим відповідями
                new QuestionViewModel
                {
                    Question = "8. Як називається точка, в якій дотична до графіку функції збігається з віссю абсцис?",
                    Type = QuestionType.OpenEnded
                },
                new QuestionViewModel
                {
                    Question = "9. Знайдіть точку перетину прямої та площини: 2x - y + 3z = 6 і x + y - z = 1",
                    Type = QuestionType.OpenEnded
                },
                new QuestionViewModel
                {
                    Question = "10. Які координати центру та радіуса кола, заданого рівнянням x^2 + y^2 - 6x + 8y - 5 = 0?",
                    Type = QuestionType.OpenEnded
                }
            };
            return questions;
        }
    }

}

public enum QuestionType
{
    SingleChoice,
    MultipleChoice,
    OpenEnded
}
