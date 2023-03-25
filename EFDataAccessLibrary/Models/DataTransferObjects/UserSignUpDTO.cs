using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class UserSignUpDTO
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Firstname { get; set; } = string.Empty;
    [Required]
    public string Lastname { get; set; } = string.Empty;
    //public string Role { get; set; } = string.Empty;
}
