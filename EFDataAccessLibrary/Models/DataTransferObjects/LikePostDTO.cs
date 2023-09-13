using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects;

public class LikePostDTO
{
    public int userId { get; set; }
    public int postId { get; set; }
}
