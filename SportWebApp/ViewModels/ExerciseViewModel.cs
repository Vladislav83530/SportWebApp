using SportWebApp.Models;

namespace SportWebApp.ViewModels
{
    public class ExerciseViewModel
    {
        public Exercise Exercise { get; set; }
        public IEnumerable<Exercise>? Exercises { get; set; }
        public FilterViewModel? FilterViewModel { get; set; }
    }
}
