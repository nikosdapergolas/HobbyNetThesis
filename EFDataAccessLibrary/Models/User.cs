using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models;

// This is the main User object
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


    public List<Hobby> Hobbies { get; set; } = new List<Hobby>();
    public List<Role> Roles { get; set; } = new List<Role>();
}
