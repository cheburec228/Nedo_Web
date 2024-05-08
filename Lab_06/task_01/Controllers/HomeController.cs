using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task_01.Models;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace task_01.Controllers
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


    }
}