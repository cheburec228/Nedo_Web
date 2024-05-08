using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Task_03.Models;
using MySql.Data.MySqlClient;

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
            TempData["row"] = formData.row;

            return RedirectToAction("Result");
        }

        public ActionResult Result()
        {
            int row = Convert.ToInt32(TempData["row"]);
            string column = string.Empty;

            using (MySqlConnection connection = connToDb())
            {
                string query = $"SELECT * FROM task_03 WHERE id = 1;";
                Console.WriteLine($"Executing SQL query: {query}");

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            column = reader["columntable"].ToString();
                        }
                    }
                }
            }
            TempData["column"] = column;
            List<List<int>> tableData = GenerateTableData(row, column);

            TempData["table"] = GenerateHtmlTable(tableData);

            return View();
        }

        private List<List<int>> GenerateTableData(int row, string column)
        {
            List<List<int>> tableData = new List<List<int>>();

            for (int i = 1; i <= row; i++)
            {
                List<int> rowData = new List<int>();
                for (int j = 1; j <= Convert.ToInt32(column); j++)
                {
                    rowData.Add(i * j);
                }
                tableData.Add(rowData);
            }

            return tableData;
        }

        private string GenerateHtmlTable(List<List<int>> tableData)
        {
            string htmlTable = "<table class='table'>";
            foreach (var row in tableData)
            {
                htmlTable += "<tr>";
                foreach (var cell in row)
                {
                    htmlTable += $"<td>{cell}</td>";
                }
                htmlTable += "</tr>";
            }
            htmlTable += "</table>";

            return htmlTable;
        }
    }
}
