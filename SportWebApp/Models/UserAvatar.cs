using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWebApp.Models
{
    public class UserAvatar
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
