using Microsoft.AspNetCore.Identity;

namespace BookStore.API.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
