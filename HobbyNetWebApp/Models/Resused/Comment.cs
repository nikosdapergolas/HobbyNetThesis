using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyNetWebApp.Models.Resused;

public class Comment
{
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string body { get; set; }

    [Required]
    public User user { get; set; }
}
