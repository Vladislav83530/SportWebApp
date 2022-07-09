using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWebApp.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserSurname { get; set; }
        public Gender Gender { get; set; }
        public string? Country { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }


        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
