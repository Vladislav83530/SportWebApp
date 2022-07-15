using SportWebApp.Models;

namespace SportWebApp.Data.Interfaces
{
    public interface ITrainingHistoryRepository
    {
        public Task<IEnumerable<TrainingHistory>> GetTrainingsAsync(string currentUserId);
        public Task CreateTrainingAsync(TrainingHistory training, string currentUserId);
        public Task DeleteTrainingsAsync();
    }
}
