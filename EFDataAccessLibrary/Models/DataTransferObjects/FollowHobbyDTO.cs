using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class FollowHobbyDTO
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int HobbyId { get; set; }
}
