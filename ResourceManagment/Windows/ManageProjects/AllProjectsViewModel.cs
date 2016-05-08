using System.Collections.ObjectModel;

namespace ResourceManagment.Windows.ManageProjects
{
    public class AllProjectsViewModel : ViewModel
    {
        private string _editedProjectName;
        private ProjectViewModel _selectedProject;
        private bool _dataHasChanged;
        public ObservableCollection<ProjectViewModel> Projects { get; private set; }

        public AllProjectsViewModel(ObservableCollection<ProjectViewModel> projects)
        {
            Projects = projects;

        }
        public string EditedProjectName
        {
            get { return _editedProjectName; }
            set
            {
                SetPropertyField(ref _editedProjectName, value);
                EvalDataChange();
            }
        }

        private void EvalDataChange()
        {
            if (_editedProjectName == null || _selectedProject == null)
            {
                return;
            }
            bool nameIsEdited = !_editedProjectName.Equals(_selectedProject.Name);
            DataHasChanged = nameIsEdited;
        }

        public ProjectViewModel SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                SetPropertyField(ref _selectedProject, value);
                EditedProjectName = _selectedProject == null ? "" : _selectedProject.Name;
                DataHasChanged = false;
            }
        }

        public bool DataHasChanged
        {
            get { return _dataHasChanged; }
            set { SetPropertyField(ref _dataHasChanged, value); }
        }

    }
}