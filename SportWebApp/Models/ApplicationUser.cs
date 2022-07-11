using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SportWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserProfile? UserProfile { get; set; }
        public ICollection<Training>? Training { get; set; } 
        public UserAvatar? UserAvatar { get; set; }
        public ICollection<Exercise>? Exercises { get; set; }
    }
}
