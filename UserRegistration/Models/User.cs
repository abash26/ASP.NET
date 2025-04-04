using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Models;

public class User(string email, string password, string confirmPassword)
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = email;

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password should be at least 6 char long")]
    public string Password { get; set; } = password;

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Password do not match")]
    public string ConfirmPassword { get; set; } = confirmPassword;

    public static ValueTask<User?> BindAsync(HttpContext context)
    {
        var email = context.Request.Query["email"];
        var password = context.Request.Query["pw1"];
        var confirmPassword = context.Request.Query["pw2"];

        return new ValueTask<User?>(new User(email, password, confirmPassword));
    }
}
