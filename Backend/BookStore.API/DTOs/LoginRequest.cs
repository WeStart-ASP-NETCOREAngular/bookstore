using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
