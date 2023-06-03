using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyNetWebApp.Models.Resused;

public class HobbiesOfUsers
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int HobbyId { get; set; }
}
