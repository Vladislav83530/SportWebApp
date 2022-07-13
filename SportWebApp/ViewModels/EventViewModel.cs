using SportWebApp.Models;

namespace SportWebApp.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        public string UserId { get; set; }

        public EventViewModel(Event myevent, string userid)
        {
            Event = myevent;
            UserId = userid;
        }

        public EventViewModel(string userid)
        {
            UserId = userid;

        }

        public EventViewModel()
        {

        }
    }
}
