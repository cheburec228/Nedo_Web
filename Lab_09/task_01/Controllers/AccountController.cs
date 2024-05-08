using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Services.Description;
using MySql.Data.MySqlClient;

public class AccountController : Controller
{
    private string connectionString = "server=localhost;user=root;password=0672951355;database=loginsdb";
    private const string InboxFilePath = "~/App_Data/Inbox.txt";

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
                    string dbPassword = reader["Password"].ToString();

                    if (dbPassword == user.Password)
                    {
                        Session["CurrentUserEmail"] = user.Email;
                        return RedirectToAction("Profile", new { userEmail = user.Email });
                        //return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
        ViewBag.LoginErrorMessage = "Incorrect email or password";
        return View(user);

    }

    public ActionResult Profile(string userEmail)
    {
        List<TestAttempt> testAttempts = new List<TestAttempt>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT AttemptNumber, CorrectAnswersCount FROM TestResults WHERE Email = @Email";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", userEmail);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TestAttempt attempt = new TestAttempt
                    {
                        AttemptNumber = Convert.ToInt32(reader["AttemptNumber"]),
                        Result = Convert.ToInt32(reader["CorrectAnswersCount"])
                    };
                    testAttempts.Add(attempt);
                }
            }
        }

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM users WHERE Email = @Email";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", userEmail);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Создаем новый экземпляр User и заполняем его данными из базы данных
                    User user = new User
                    {
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        BirthYear = Convert.ToInt32(reader["BirthYear"]),
                        BirthMonth = Convert.ToInt32(reader["BirthMonth"]),
                        BirthDay = Convert.ToInt32(reader["BirthDay"])
                    };
                    UserProfileViewModel userProfile = new UserProfileViewModel
                    {
                        User = user,
                        TestAttempts = testAttempts
                    };

                    return View(userProfile);
                }
            }
        }
        return RedirectToAction("Index", "Home");
    }
    public ActionResult EditProfile(string userEmail)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT * FROM users WHERE Email = @Email";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", userEmail);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    User user = new User
                    {
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        BirthYear = Convert.ToInt32(reader["BirthYear"]),
                        BirthMonth = Convert.ToInt32(reader["BirthMonth"]),
                        BirthDay = Convert.ToInt32(reader["BirthDay"])
                    };

                    return View(user);
                }
            }
        }
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public ActionResult EditProfile(User updatedUser)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "UPDATE users SET Name = @Name, BirthYear = @BirthYear, BirthMonth = @BirthMonth, BirthDay = @BirthDay WHERE Email = @Email";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", updatedUser.Name);
            command.Parameters.AddWithValue("@BirthYear", updatedUser.BirthYear);
            command.Parameters.AddWithValue("@BirthMonth", updatedUser.BirthMonth);
            command.Parameters.AddWithValue("@BirthDay", updatedUser.BirthDay);
            command.Parameters.AddWithValue("@Email", updatedUser.Email);
            connection.Open();
            command.ExecuteNonQuery();
        }
        return RedirectToAction("Profile", new { userEmail = updatedUser.Email });
        
    }
    public ActionResult Inbox()
    {
        // Получаем email текущего пользователя из сессии
        string currentUserEmail = Session["CurrentUserEmail"] as string;

        // Создаем список сообщений для текущего пользователя
        List<Message> userMessages = new List<Message>();

        // Читаем сообщения из файла и добавляем только те, которые адресованы текущему пользователю
        string[] lines = System.IO.File.ReadAllLines(Server.MapPath(InboxFilePath));
        foreach (string line in lines)
        {
            string[] parts = line.Split('\t');
            if (parts.Length >= 5 && parts[1] == currentUserEmail)
            {
                userMessages.Add(new Message
                {
                    From = parts[0],
                    To = parts[1],
                    Subject = parts[2],
                    Text = parts[3],
                    Date = DateTime.Parse(parts[4])
                });
            }
        }

        // Возвращаем представление с сообщениями текущего пользователя
        return View(userMessages);
    }

    public ActionResult Compose(string userEmail)
    {
        ViewBag.UserEmail = userEmail;
        return View();
    }
    [HttpPost]
    public ActionResult SendMessage(Message message)
    {
        string currentUserEmail = Session["CurrentUserEmail"] as string;

        message.From = currentUserEmail;
        // Добавляем новое сообщение в файл
        using (StreamWriter sw = System.IO.File.AppendText(Server.MapPath(InboxFilePath)))
        {
            sw.WriteLine($"{message.From}\t{message.To}\t{message.Subject}\t{message.Text}\t{DateTime.Now}");
        }


        // Перенаправляем на страницу Inbox
        return RedirectToAction("Inbox");
    }

    private List<Message> ReadMessagesFromFile()
    {
        List<Message> messages = new List<Message>();

        try
        {
            using (StreamReader sr = new StreamReader(Server.MapPath(InboxFilePath)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('\t');
                    if (parts.Length == 4)
                    {
                        Message message = new Message
                        {
                            To = parts[0],
                            Subject = parts[1],
                            Text = parts[2],
                            Date = DateTime.Parse(parts[3])
                        };
                        messages.Add(message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Обработка ошибок чтения файла
            ViewBag.ErrorMessage = "An error occurred while reading messages.";
        }

        return messages;
    }


}
