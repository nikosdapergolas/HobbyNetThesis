using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models;

public class Message
{
    public int Id { get; set; }

    [Required]
    public DateTime timestamp { get; set; }

    [Required]
    [MaxLength(100)]
    public string messageSender { get; set; }

    [Required]
    [MaxLength(250)]
    public string messageBody { get; set; }
}
