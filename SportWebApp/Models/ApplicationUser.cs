using Microsoft.AspNetCore.Identity;

namespace SportWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserProfile? UserProfile { get; set; }
        public Training? Training { get; set; } 
        public Exercise? Exercise { get; set; }
    }
}
