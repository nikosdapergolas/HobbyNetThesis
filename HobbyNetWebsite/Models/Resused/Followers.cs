using System.ComponentModel.DataAnnotations;

namespace HobbyNetWebsite.Models.Resused;

public class Followers
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FollowerUsername { get; set; }

    [Required]
    [MaxLength(100)]
    public string FolloweeUsername { get; set; }
}
