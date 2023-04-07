using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models.DataTransferObjects
{
    public class UserSearchResultDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string username { get; set; }
    }
}
