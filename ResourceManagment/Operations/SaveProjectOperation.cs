using ResourceManagment.Data.Database;
using ResourceManagment.Windows.ManageProjects;

namespace ResourceManagment.Operations
{
    public class SaveProjectOperation : AsyncDiscreetOperation
    {
        private readonly ProjectViewModel _selectedProject;
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public SaveProjectOperation(ProjectViewModel selectedProject, ResourceManagerDatabaseSchema databaseSchema)
        {
            _selectedProject = selectedProject;
            _databaseSchema = databaseSchema;
        }

        public override string Description => "Saving project : " + _selectedProject.Name;

        protected override void DoWorkInternal()
        {
            _databaseSchema.ProjectTable.Save(_selectedProject);
        }
    }
}