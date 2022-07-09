using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWebApp.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MuscleGroup { get; set; }
        public string? Equipment { get; set; }
        public double Weight { get; set; }
        public int Repetition { get; set; } 
        public int ApproachCount { get; set; }
        public string? ImageUrl { get; set; }


        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
