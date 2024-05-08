using System.ComponentModel.DataAnnotations;

public class User
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(int.MaxValue, MinimumLength = 12, ErrorMessage = "Password must be at least 12 characters long.")]
    [RegularExpression("^[a-zA-Z0-9!@#$%^&*()_+]+$", ErrorMessage = "Password must contain only numbers, Latin characters, and special characters.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Password confirmation is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
    [Required(ErrorMessage = "Birth Year is required.")]
    [Range(1000, 9999, ErrorMessage = "Year must be between 1000 and 9999.")]
    public int BirthYear { get; set; }

    [Required(ErrorMessage = "Birth Month is required.")]
    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
    public int BirthMonth { get; set; }

    [Required(ErrorMessage = "Birth Day is required.")]
    [Range(1, 31, ErrorMessage = "Day must be between 1 and 31.")]
    public int BirthDay { get; set; }
}
