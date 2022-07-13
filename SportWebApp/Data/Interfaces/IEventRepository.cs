using SportWebApp.Models;

namespace SportWebApp.Data.Interfaces
{
    public interface IEventRepository
    {
        public Task<List<Event>> GetEventsAsync();
        public Task<List<Event>> GetMyEventsAsync(string userid);
        public Task<Event> GetEventAsync(int id); 
        public Event GetEvent(int id);
        public Task CreateEventAsync(IFormCollection form);
        public Task UpdateEventAsync(IFormCollection form);
        public Task DeleteEventAsync(int id);
    }
}
