using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HobbyNetWebsite.Models.Resused;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string username { get; set; }

    [Required]
    [MaxLength(500)]
    public string passwordHash { get; set; }

    [Required]
    [MaxLength(150)]
    public string email { get; set; }

    [Required]
    [MaxLength(45)]
    public string firstname { get; set; }

    [Required]
    [MaxLength(45)]
    public string lastname { get; set; }

    [MaxLength(2000)]
    [AllowNull]
    public string? profileImage { get; set; }

    public List<HobbiesOfUsers> Hobbies { get; set; } = new List<HobbiesOfUsers>();
    public List<Role> Roles { get; set; } = new List<Role>();
}
