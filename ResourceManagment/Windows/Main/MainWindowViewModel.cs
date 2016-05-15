using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ResourceManagment.Data.Database;
using ResourceManagment.Data.Filtering.ResourceFilters;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.Main
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly ResourceManagerDatabaseSchema _databaseSchema;
        private WeekScheduleViewModel _selectedSchedule;


        public ObservableCollection<WeekScheduleViewModel> AllSchedules { get; private set; }
        public ObservableCollection<ProjectViewModel> Projects { get; internal set; }
        public ObservableCollection<PersonViewModel> People { get; internal set; }

        public MainWindowViewModel(ResourceManagerDatabaseSchema databaseSchema)
        {
            _databaseSchema = databaseSchema;
            AllSchedules = new ObservableCollection<WeekScheduleViewModel>();
            Projects = new ObservableCollection<ProjectViewModel>();
            People = new ObservableCollection<PersonViewModel>();

            People.CollectionChanged += PersistPerson;
            Projects.CollectionChanged += PersistProject;
            AllSchedules.CollectionChanged += PersistSchedules;
        }

        private void PersistSchedules(object sender, NotifyCollectionChangedEventArgs e)
        {
            _databaseSchema.WeeklyScheduleTable.Create(e.NewItems.Cast<IWeeklySchedule>().ToList());
        }

        private void PersistProject(object sender, NotifyCollectionChangedEventArgs e)
        {
            _databaseSchema.ProjectTable.Create(e.NewItems.Cast<IProject>().ToList());
        }

        private void PersistPerson(object sender, NotifyCollectionChangedEventArgs e)
        {
            _databaseSchema.PersonTable.Create(e.NewItems.Cast<IPerson>().ToList());
        }


        public WeekScheduleViewModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set { SetPropertyField(ref _selectedSchedule, value); }
        }
    }

    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        Role Role { get; set; }
    }
}