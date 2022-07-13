using SportWebApp.Models;

namespace SportWebApp.Data.Interfaces
{
    public interface ITrainingRepository
    {
        public Task<IEnumerable<Training>> GetTrainingsAsync(string currentUserId);
        public Task CreateTrainingAsync (Training training, string currentUserId);
        public Task<Training?> GetTrainingAsync(int id);
        public Task EditTrainingAsync(Training training, int id);
        public Task DeleteTrainingAsync(int id);
    }
}
