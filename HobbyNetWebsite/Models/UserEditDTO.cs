using System.ComponentModel.DataAnnotations;

namespace HobbyNetWebsite.Models
{
    public class UserEditDTO
    {
        [MaxLength(100)]
        public string username { get; set; }

        [MaxLength(150)]
        public string email { get; set; }

        [MaxLength(45)]
        public string firstname { get; set; }

        [MaxLength(45)]
        public string lastname { get; set; }
    }
}
