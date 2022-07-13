using Microsoft.EntityFrameworkCore;
using SportWebApp.Data.Interfaces;
using SportWebApp.Models;

namespace SportWebApp.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get All Events
        /// </summary>
        /// <returns>List of Events</returns>
        public async Task<List<Event>> GetEventsAsync()
        {
            return await db.Events.ToListAsync();
        }

        /// <summary>
        /// get all events
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>List of events</returns>
        public async Task<List<Event>> GetMyEventsAsync(string userid)
        {
            return await db.Events.Where(x => x.User.Id == userid).ToListAsync();
        }

        /// <summary>
        /// get one event by id (async)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Event</returns>
        public async Task<Event> GetEventAsync(int id)
        {
            return await db.Events.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// get one event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Event</returns>
        public Event GetEvent(int id)
        {
            return  db.Events.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// create event
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task CreateEventAsync(IFormCollection form)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == form["UserId"].ToString());
            var newevent = new Event(form, user);
            db.Events.Add(newevent);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// update event
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public async Task UpdateEventAsync(IFormCollection form)
        {
            var eventid = int.Parse(form["Event.Id"]);
            var myevent = db.Events.FirstOrDefault(x => x.Id == eventid);
            var user = db.Users.FirstOrDefault(x => x.Id == form["UserId"].ToString());
            myevent.UpdateEvent(form, user);
            db.Entry(myevent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// delete event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteEventAsync(int id)
        {
            var myevent = await db.Events.FindAsync(id);
            db.Events.Remove(myevent);
            await db.SaveChangesAsync();
        }
    }
}
