using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreCalendar.Helpers
{
    public static class JSONListHelper
    {
        public static string GetEventListJSONString(List<SportWebApp.Models.Event> events)
        {
            var eventlist = new List<Event>();
            foreach (var model in events)
            {
                var myevent = new Event()
                {
                    id = model.Id,
                    start = model.StartTime,
                    end = model.EndTime,
                    description = model.Description,
                    title = model.Name
                };
                eventlist.Add(myevent);
            }
            return System.Text.Json.JsonSerializer.Serialize(eventlist);
        }
    }

    public class Event
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int resourceId { get; set; }
        public string description { get; set; }
    }
}
