using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ResourceManagment.Data.Database;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.Main
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly ModelDataBinder _modelBinder;
        private WeekScheduleViewModel _selectedSchedule;


        public ObservableCollection<WeekScheduleViewModel> AllSchedules { get; private set; }
        public ObservableCollection<ProjectViewModel> Projects { get; internal set; }
        public ObservableCollection<PersonViewModel> People { get; internal set; }

        public MainWindowViewModel(ModelDataBinder modelBinder)
        {
            _modelBinder = modelBinder;
            AllSchedules = new ObservableCollection<WeekScheduleViewModel>();
            Projects = new ObservableCollection<ProjectViewModel>();
            People = new ObservableCollection<PersonViewModel>();

            People.CollectionChanged += _modelBinder.PersistPerson;
            Projects.CollectionChanged += _modelBinder.PersistProject;
            AllSchedules.CollectionChanged += _modelBinder.PersistSchedules;
        }

        public WeekScheduleViewModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set { SetPropertyField(ref _selectedSchedule, value); }
        }
    }
}