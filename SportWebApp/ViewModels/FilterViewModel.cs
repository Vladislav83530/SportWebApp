namespace SportWebApp.ViewModels
{
	public class FilterViewModel
	{
        public FilterViewModel(string name, string muscleGroup, string equipment)
        {
            SelectedName = name;
            SelectedMuscleGroup= muscleGroup;
            SelectedEquipment = equipment;
        }
        public string SelectedName { get; private set; }
        public string SelectedMuscleGroup { get; private set; }
        public string SelectedEquipment { get; private set; }
    }
}
