using System.ComponentModel.DataAnnotations;

namespace HobbyNetWebsite.Models;

public class FollowHobbyDTO
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int HobbyId { get; set; }
}
