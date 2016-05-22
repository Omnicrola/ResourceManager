using System.Collections.Generic;
using ResourceManagment.Data.Database;
using ResourceManagment.Windows.Main;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;

namespace ResourceManagment.Operations
{
    public class LoadInitialDataOperation : AsyncDiscreetOperation
    {
        private readonly ResourceManagerDatabaseSchema _databaseSchema;
        private List<PersonViewModel> _personViewModels;
        private List<ProjectViewModel> _projectViewModels;

        public LoadInitialDataOperation(ResourceManagerDatabaseSchema databaseSchema)
        {
            _databaseSchema = databaseSchema;
        }

        public override string Description => "Performing first loading of existing data.";

        protected override void DoWorkInternal()
        {
            _personViewModels = _databaseSchema.PersonTable.GetAll<PersonViewModel>();
            _projectViewModels = _databaseSchema.ProjectTable.GetAll<ProjectViewModel>();
        }

        public void Populate(MainWindowViewModel mainWindowViewModel)
        {
            _personViewModels.ForEach(p => mainWindowViewModel.People.Add(p));
            _projectViewModels.ForEach(p => mainWindowViewModel.Projects.Add(p));
        }
    }
}