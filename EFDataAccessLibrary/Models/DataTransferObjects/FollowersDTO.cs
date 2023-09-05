using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class FollowersDTO
{
    [Required]
    [MaxLength(100)]
    public string FollowerUsername { get; set; }

    [Required]
    [MaxLength(100)]
    public string FolloweeUsername { get; set; }
}
