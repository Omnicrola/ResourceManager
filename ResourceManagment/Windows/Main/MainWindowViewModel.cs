using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ResourceManagment.Data.Models;
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
        public ObservableCollection<ProjectViewModel> Projects { get; internal set; }
        public ObservableCollection<PersonViewModel> People { get; internal set; }

        public MainWindowViewModel()
        {
            AllSchedules = new ObservableCollection<WeekScheduleViewModel>();
            Projects = new ObservableCollection<ProjectViewModel>();
            People = new ObservableCollection<PersonViewModel>();

        }

        public WeekScheduleViewModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set { SetPropertyField(ref _selectedSchedule, value); }
        }
    }
}