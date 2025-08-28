using System.ComponentModel.DataAnnotations;

namespace Findexium.DTOs
{
    public class UserDTO
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? Role { get; set; }
    }
}
