using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using task_02.Models;
using System.Text.RegularExpressions;

namespace task_02.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = "server=localhost;user=root;password=0672951355;database=lab_06";

        public ActionResult Index()
        {
            List<Detail> dataList = new List<Detail>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM task_01";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Detail data = new Detail
                    {
                        Id = (int)reader["d_id"],
                        Name = reader["d_name"].ToString(),
                        Type = reader["d_type"].ToString(),
                        Quantity = (int)reader["d_count"],
                        Weight = (int)reader["d_weight"]
                    };
                    dataList.Add(data);
                }
            }

            return View(dataList);
        }
        [HttpPost]
        public ActionResult AddDetail(string name, string type, int quantity, int weight)
        {
            // Валидация данных перед добавлением
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

            // Если все данные прошли валидацию, добавляем деталь в базу данных
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO task_01 (d_name, d_type, d_count, d_weight) VALUES (@Name, @Type, @Quantity, @Weight)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Weight", weight);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // После добавления детали перенаправляем пользователя на главную страницу
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ClearTable()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"DELETE * FROM task_01";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            // После очистки перенаправляем пользователя на главную страницу
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteDetail(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $"DELETE FROM task_01 WHERE d_id = {id}";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // После удаления перенаправляем пользователя на главную страницу
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

            // Если все данные прошли валидацию, сохраняем изменения в базе данных
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE task_01 SET d_name = @Name, d_type = @Type, d_count = @Quantity, d_weight = @Weight WHERE d_id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Type", type);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Weight", weight);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }

            // После сохранения изменений перенаправляем пользователя на главную страницу
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