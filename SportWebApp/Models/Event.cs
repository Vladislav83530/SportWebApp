using System.ComponentModel.DataAnnotations;

namespace SportWebApp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Relational data
        public virtual ApplicationUser User { get; set; }

        public Event(IFormCollection form, ApplicationUser user)
        {
            User = user;
            Name = form["Event.Name"].ToString();
            Description = form["Event.Description"].ToString();
            StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
        }

        public void UpdateEvent(IFormCollection form, ApplicationUser user)
        {
            User = user;
            Name = form["Event.Name"].ToString();
            Description = form["Event.Description"].ToString();
            StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
        }

        public Event()
        {

        }
    }
}
