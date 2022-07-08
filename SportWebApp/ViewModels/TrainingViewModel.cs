using SportWebApp.Models;

namespace SportWebApp.ViewModels
{
	public class TrainingViewModel
	{
		public IEnumerable<Training>? Trainings { get; set; }
		public IEnumerable<Exercise>? Exercises { get; set; }
		public FilterViewModel? FilterViewModel { get; set; }	
		public Training? Training { get; set; }
		public Exercise? Exercise { get; set; }
	}
}
