using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class CommentDTO
{
    public string username { get; set; }
    public int postId { get; set; }
    public string body { get; set; }
}
