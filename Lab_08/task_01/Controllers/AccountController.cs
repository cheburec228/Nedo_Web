using System;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

public class AccountController : Controller
{
    private string connectionString = "server=localhost;user=root;password=0672951355;database=loginsdb";

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE Name = @Name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                {
                    ModelState.AddModelError("Name", "Username already exists.");
                    return View(user);
                }
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO users (Name, Email, Password, BirthYear, BirthMonth, BirthDay) VALUES (@Name, @Email, @Password, @BirthYear, @BirthMonth, @BirthDay)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@BirthYear", user.BirthYear);
                command.Parameters.AddWithValue("@BirthMonth", user.BirthMonth);
                command.Parameters.AddWithValue("@BirthDay", user.BirthDay);
                connection.Open();
                command.ExecuteNonQuery();
            }

            ViewBag.RegisterSuccessMessage = "Registration successful!";
        }
        return View(user);
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(User user)
    {

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM users WHERE Email = @Email";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", user.Email);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Проверка пароля
                    string dbPassword = reader["Password"].ToString();

                    if (dbPassword == user.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
        ViewBag.LoginErrorMessage = "Incorrect email or password";
        return View(user); 
    }
}
