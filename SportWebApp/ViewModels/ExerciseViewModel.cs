using SportWebApp.Models;

namespace SportWebApp.ViewModels
{
    public class ExerciseViewModel
    {
        public IEnumerable<Exercise>? Exercises { get; set; }
        public FilterViewModel? FilterViewModel { get; set; }
    }
}
