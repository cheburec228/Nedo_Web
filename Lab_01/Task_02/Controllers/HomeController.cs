using System;
using System.Web.Mvc;
using Task_02.Models;
using MySql.Data.MySqlClient; // Import the library for working with MySQL

namespace Lab_01.Controllers
{
    public class HomeController : Controller
    {
        // Function to connect to the database
        private MySqlConnection connToDb()
        {
            string connectionString = "server=localhost; user=root; password=0672951355; database=lab_01";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormData formData)
        {
            TempData["Path1"] = formData.Path1;
            TempData["Path2"] = formData.Path2;
            TempData["Path3"] = formData.Path3;
            TempData["Path4"] = formData.Path4;
            TempData["Path5"] = formData.Path5;

            return RedirectToAction("Result");
        }
        public ActionResult Result()
        {
            string cssParam1 = string.Empty;
            string cssParam2 = string.Empty;
            string cssParam3 = string.Empty;
            string cssParam4 = string.Empty;
            string cssParam5 = string.Empty;

            string cssParam1mod = string.Empty;
            string cssParam2mod = string.Empty;
            string cssParam3mod = string.Empty;


            using (MySqlConnection connection = connToDb())
            {
                string query = $"SELECT * FROM task_02 WHERE id = 1;";
                Console.WriteLine($"Executing SQL query: {query}");

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cssParam1 = reader["image1"].ToString();
                            cssParam2 = reader["image2"].ToString();
                            cssParam3 = reader["image3"].ToString();
                            cssParam4 = reader["image4"].ToString();
                            cssParam5 = reader["image5"].ToString();
                        }
                    }
                }
            }
            using (MySqlConnection connection = connToDb())
            {
                string query = $"SELECT * FROM task_02 WHERE id = 2;";
                Console.WriteLine($"Executing SQL query: {query}");

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cssParam1mod = reader["image1"].ToString();
                            cssParam2mod = reader["image2"].ToString();
                            cssParam3mod = reader["image3"].ToString();
                        }
                    }
                }
            }
            // Присвойте значения TempData после их получения из базы данных
            TempData["imageCss1"] = cssParam1;
            TempData["imageCss2"] = cssParam2;
            TempData["imageCss3"] = cssParam3;
            TempData["imageCss4"] = cssParam4;
            TempData["imageCss5"] = cssParam5;

            TempData["imageCss1mod"] = cssParam1mod;
            TempData["imageCss2mod"] = cssParam2mod;
            TempData["imageCss3mod"] = cssParam3mod;
            return View();
        }


    }
}
