using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWebApp.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? MuscleGroup { get; set; }
        public string? Place { get; set; }
        public string? Notes { get; set; }
        public string? Exercises { get; set; }  
        public string? ImageUrl { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
