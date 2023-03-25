using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models;

public class Role
{
    public int Id { get; set; }

    [Required]
    [MaxLength(45)]
    public string roleName { get; set; }
}
