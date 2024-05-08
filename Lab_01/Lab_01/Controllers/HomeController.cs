using System;
using System.Web.Mvc;
using Lab_01.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lab_01.Controllers
{
    public class HomeController : Controller
    {
        static string DBConnect = "server=localhost; user=root; password=0672951355; database=lab_01";
        static public MySqlDataAdapter msDataAdapter;
        static MySqlConnection myconnect;
        static public MySqlCommand msCommand;

        public static bool ConnectionDB()
        {
            try
            {
                myconnect = new MySqlConnection(DBConnect);
                myconnect.Open();
                msCommand = new MySqlCommand();
                msCommand.Connection = myconnect;
                msDataAdapter = new MySqlDataAdapter(msCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormData formData)
        {
            TempData["FormContent"] = formData.FormContent;
            TempData["FormTextParameters"] = formData.FormTextParameters;
            TempData["BackgroundColor"] = 1;
            TempData["SpaceWord"] = 2;

            return RedirectToAction("Result");
        }
        public ActionResult Result()
        {
            ConnectionDB();

            string dbSpaceWord = string.Empty;
            string backgroundColor = string.Empty;

            using (MySqlConnection connection = myconnect)
            {
                string query = $"SELECT spaces_param, bg_param FROM task_01 WHERE id = 1";
                Console.WriteLine($"Executing SQL query: {query}");

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dbSpaceWord = reader["spaces_param"].ToString();
                            backgroundColor = reader["bg_param"].ToString();
                        }
                    }
                }
            }

            // Присвойте значения TempData после их получения из базы данных
            TempData["BackgroundColor"] = backgroundColor;
            TempData["SpaceWord"] = dbSpaceWord;
            

            return View();
        }

           
    }
}
