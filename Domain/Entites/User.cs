using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entites;

public class User : IdentityUser
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(12)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$",
        ErrorMessage = "Password must contain at least 12 characters, including letters, numbers, and special characters.")]
    public string Password { get; set; }

    [Required, MaxLength(30)]
    [RegularExpression(@"^[a-zA-Zа-яА-Я0-9]+$")]
    public string UserName { get; set; }

    [MaxLength(500)]
    public string Profile { get; set; }
    public string? Address { get; set; }
}