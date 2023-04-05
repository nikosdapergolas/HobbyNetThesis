using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class UserEditDTO
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string username { get; set; }

    [MaxLength(150)]
    public string email { get; set; }

    [MaxLength(45)]
    public string firstname { get; set; }

    [MaxLength(45)]
    public string lastname { get; set; }
}
