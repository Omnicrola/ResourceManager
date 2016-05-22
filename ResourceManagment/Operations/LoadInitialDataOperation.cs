using System.Collections.Generic;
using ResourceManagment.Data.Database;
using ResourceManagment.Windows.Main;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Operations
{
    public class LoadInitialDataOperation : AsyncDiscreetOperation
    {
        private readonly ResourceManagerDatabaseSchema _databaseSchema;
        private List<PersonViewModel> _personViewModels;
        private List<ProjectViewModel> _projectViewModels;
        private List<WeekScheduleViewModel> _weekScheduleViewModels;

        public LoadInitialDataOperation(ResourceManagerDatabaseSchema databaseSchema)
        {
            _databaseSchema = databaseSchema;
        }

        public override string Description => "Performing first loading of existing data.";

        protected override void DoWorkInternal()
        {
            _personViewModels = _databaseSchema.PersonTable.GetAll<PersonViewModel>();
            _projectViewModels = _databaseSchema.ProjectTable.GetAll<ProjectViewModel>();
            _weekScheduleViewModels = _databaseSchema.WeeklyScheduleTable.GetAll<WeekScheduleViewModel>();
        }

        public void Populate(MainWindowViewModel mainWindowViewModel)
        {
            _personViewModels.ForEach(p => mainWindowViewModel.People.Add(p));
            _projectViewModels.ForEach(p => mainWindowViewModel.Projects.Add(p));
            _weekScheduleViewModels.ForEach(p => mainWindowViewModel.AllSchedules.Add(p));
        }
    }
}