using System.Collections.ObjectModel;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.Main
{
    public class MainWindowViewModel : ViewModel
    {
        private WeekScheduleViewModel _selectedSchedule;

        public ObservableCollection<WeekScheduleViewModel> AllSchedules { get; private set; }
        public WeekScheduleViewModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set { SetPropertyField(ref _selectedSchedule, value); }
        }

        public ObservableCollection<ProjectViewModel> Projects { get; internal set; }
        public ObservableCollection<PersonViewModel> People { get; internal set; }

        public MainWindowViewModel()
        {
            AllSchedules = new ObservableCollection<WeekScheduleViewModel>();
            Projects = new ObservableCollection<ProjectViewModel>();
            People = new ObservableCollection<PersonViewModel>();
        }
    }
}