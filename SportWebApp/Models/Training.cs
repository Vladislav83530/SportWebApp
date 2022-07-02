using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWebApp.Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }   
        public string? Exercises { get; set; }
        public DateTime Date { get; set; }
        public DateTime Duration { get; set; }
        public string? Place { get; set; }
        public double Calories { get; set; }
        public string? Notes { get; set; }  
        public Feeling Feeling { get; set; }

        public string? MuscleGroup { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
    public enum Feeling
    {
        Badly,
        Normally,
        Soso,
        Perfectly,
    }
}
