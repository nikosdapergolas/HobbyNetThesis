using System.ComponentModel.DataAnnotations;

namespace HobbyNetWebsite.Models;

public class FollowersDTO
{
    [Required]
    [MaxLength(100)]
    public string FollowerUsername { get; set; }

    [Required]
    [MaxLength(100)]
    public string FolloweeUsername { get; set; }
}
