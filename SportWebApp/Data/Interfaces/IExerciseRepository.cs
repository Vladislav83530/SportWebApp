using SportWebApp.Models;

namespace SportWebApp.Data.Interfaces
{
    public interface IExerciseRepository
    {
        public Task<IEnumerable<Exercise>> GetExercisesAsync(string currentUserID);
        public Task<Exercise?> GetExerciseAsync(int id);
        public Task CreateExerciseAsync(Exercise exercise, string currentUserId);
        public Task EditExerciseAsync (Exercise exercise, int id);
        public Task DeleteExerciseAsync(int? id);
        public Task AddExerciseAsync(Exercise _exercise, Training _training);
        public Task DeleteExerciseForTrainingAsync(int? id, int? idexer);
    }
}
