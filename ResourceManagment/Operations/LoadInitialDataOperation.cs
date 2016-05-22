using System.Collections.Generic;
using System.Linq;
using ResourceManagment.Data.Database;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.AlterResourceBlock;
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
            _databaseSchema.ResourceBlockTable
                .GetAll<ResourceBlockModel>()
                .ForEach(MapResourcesToSchedules);
        }

        private void MapResourcesToSchedules(ResourceBlockModel resourceBlockModel)
        {
            var person = FindPersonById(resourceBlockModel.PersonId);
            var pairPartner = FindPersonById(resourceBlockModel.PairPartnerId);
            var project = FindProjectById(resourceBlockModel.ProjectId);
            var weeklySchedule = FindScheduleById(resourceBlockModel.WeeklyScheduleId);
            var resourceBlockViewModel = new ResourceBlockViewModel(person, resourceBlockModel.Date)
            {
                Project = project,
                PairPartner = pairPartner
            };
            weeklySchedule.OverwriteBlock(resourceBlockViewModel);
        }

        private WeekScheduleViewModel FindScheduleById(int? weeklyScheduleId)
        {
            if (weeklyScheduleId.HasValue)
            {
                return _weekScheduleViewModels.FirstOrDefault(w => w.Id == weeklyScheduleId);
            }
            return null;
        }

        private PersonViewModel FindPersonById(int? personId)
        {

            if (personId.HasValue)
            {
                return _personViewModels.FirstOrDefault(p => p.ID == personId.Value);
            }
            return null;
        }

        private ProjectViewModel FindProjectById(int? projectId)
        {
            if (projectId.HasValue)
            {
                return _projectViewModels.FirstOrDefault(p => p.Id == projectId.Value);
            }
            return null;
        }

        public void Populate(MainWindowViewModel mainWindowViewModel)
        {
            _personViewModels.ForEach(p => mainWindowViewModel.People.Add(p));
            _projectViewModels.ForEach(p => mainWindowViewModel.Projects.Add(p));
            _weekScheduleViewModels.ForEach(p => mainWindowViewModel.AllSchedules.Add(p));
        }
    }
}