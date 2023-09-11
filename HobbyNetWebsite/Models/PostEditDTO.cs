using System.ComponentModel.DataAnnotations;

namespace HobbyNetWebsite.Models;

public class PostEditDTO
{
    [Required]
    public int Id { get; set; }

    [MaxLength(250)]
    [Required]
    public string body { get; set; }
}
