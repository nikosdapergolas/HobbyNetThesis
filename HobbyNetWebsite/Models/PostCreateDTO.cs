using System.ComponentModel.DataAnnotations;

namespace HobbyNetWebsite.Models;

public class PostCreateDTO
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string HobbyName { get; set; }

    [Required]
    [MaxLength(250)]
    public string body { get; set; }
}
