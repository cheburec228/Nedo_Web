using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace task_07.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = "server=localhost;user=root;password=0672951355;database=lab_05";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string address, string password)
        {
            if (Authenticate(address, password))
            {
                // Если аутентификация успешна, перенаправляем на страницу Success
                return RedirectToAction("Success", new { address = address });
            }
            else
            {
                // Если аутентификация неуспешна, оставляем пользователя на странице Index
                ViewBag.ErrorMessage = "Неправильный адрес или пароль";
                return View();
            }
        }

        public ActionResult Success(string address)
        {
            // Создаем объект для хранения данных аккаунта
            var account = new Dictionary<string, string>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM task_04 WHERE Address = @Address";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Address", address);

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // Заполняем словарь данными из базы данных
                        account["Address"] = reader["Address"].ToString();
                        account["Password"] = reader["Password"].ToString();
                        account["Username"] = reader["Username"].ToString();
                        account["Email"] = reader["Email"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибки
                    throw ex;
                }
            }

            // Передаем данные аккаунта в представление через ViewBag
            ViewBag.Account = account;

            return View();
        }


        private bool Authenticate(string address, string password)
        {
            bool isAuthenticated = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM task_04 WHERE Address = @Address AND Password = @Password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    isAuthenticated = reader.HasRows;
                }
                catch (Exception ex)
                {
                    // Обработка ошибки
                    throw ex;
                }
            }

            return isAuthenticated;
        }
    }
}
