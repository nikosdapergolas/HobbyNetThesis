using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models;

public class ChatMember
{
    public int Id { get; set; }

    [Required]
    public User member { get; set; }
}
