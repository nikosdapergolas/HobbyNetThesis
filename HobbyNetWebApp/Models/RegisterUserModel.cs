using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HobbyNetWebApp.Models;

public class RegisterUserModel
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [DisplayName("Email address:")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DisplayName("First Name:")]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    [DisplayName("last Name:")]
    public string Lastname { get; set; } = string.Empty;

}
