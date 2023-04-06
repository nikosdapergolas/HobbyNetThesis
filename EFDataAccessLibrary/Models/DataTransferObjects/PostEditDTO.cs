using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class PostEditDTO
{
    public int Id { get; set; }

    public Hobby hobby { get; set; }

    [MaxLength(250)]
    public string body { get; set; }
}
