using task_04.Models;
using System;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace task_04.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = "server=localhost;user=root;password=0672951355;database=lab_05";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegistrationModel model)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO task_04 (address, password, username, email) VALUES (@Address, @Password, @Username, @Email)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@Username", model.Username);
                    command.Parameters.AddWithValue("@Email", model.Email);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                TempData["Message"] = "Registration successful!";
            }
            catch (Exception ex)
            {
                // Handle exception
                TempData["Message"] = "Error occurred: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
