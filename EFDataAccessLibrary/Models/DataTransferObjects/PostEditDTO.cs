using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class PostEditDTO
{
    [Required]
    public int Id { get; set; }

    [MaxLength(250)]
    [Required]
    public string body { get; set; }
}
