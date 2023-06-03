using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

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
