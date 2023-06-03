using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyNetWebApp.Models.Resused;

public class Role
{
    public int Id { get; set; }

    [Required]
    [MaxLength(45)]
    public string roleName { get; set; }

    [Required]
    [Column("UserId")]
    public int UserId { get; set; }
}
