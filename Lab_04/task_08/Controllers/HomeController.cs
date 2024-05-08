using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using task_08.Models;

public class HomeController : Controller
{
    private string connectionString = "server=localhost;user=root;password=0672951355;database=lab_04";

    public ActionResult Index()
    {
        List<Employee> employees = new List<Employee>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM task_08";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.E_Name = reader["e_name"].ToString();
                        employee.E_Salary = Convert.ToDecimal(reader["e_salary"]);
                        employee.E_Age = Convert.ToInt32(reader["e_age"]);
                        employee.E_Gender = reader["e_gender"].ToString();
                        employee.E_Dept = reader["e_dept"].ToString();
                        employees.Add(employee);
                    }
                }
            }
        }

        return View(employees);
    }

    [HttpPost]
    public ActionResult Index(string deptFilter)
    {
        List<Employee> filteredEmployees = new List<Employee>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM task_08";

            if (!string.IsNullOrEmpty(deptFilter))
            {
                query += " WHERE e_dept = @deptFilter";
            }

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (!string.IsNullOrEmpty(deptFilter))
                {
                    command.Parameters.AddWithValue("@deptFilter", deptFilter);
                }

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.E_Name = reader["e_name"].ToString();
                        employee.E_Salary = Convert.ToDecimal(reader["e_salary"]);
                        employee.E_Age = Convert.ToInt32(reader["e_age"]);
                        employee.E_Gender = reader["e_gender"].ToString();
                        employee.E_Dept = reader["e_dept"].ToString();
                        filteredEmployees.Add(employee);
                    }
                }
            }
        }

        return View(filteredEmployees);
    }
}
