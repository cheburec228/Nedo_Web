namespace task_08.Models
{
    public class AuthorizationModel
    {
        public int Step { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }
    }

    public class AccountData
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
