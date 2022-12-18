using System.Text.Json.Serialization;

namespace BookStore.API.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
