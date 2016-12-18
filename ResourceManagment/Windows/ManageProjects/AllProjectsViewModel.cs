using System.Collections.ObjectModel;
using System.Windows.Media;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageProjects
{
    public class AllProjectsViewModel : PropertyNotification
    {
        private string _editedProjectName;
        private ProjectViewModel _selectedProject;
        private bool _dataHasChanged;
        private Color? _editedColor;
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

        public Color? EditedColor
        {
            get { return _editedColor; }
            set
            {
                SetPropertyField(ref _editedColor, value);
                EvalDataChange();
            }
        }

        private void EvalDataChange()
        {
            if (IsDifferent(_editedProjectName, SelectedProject.Name) ||
                IsDifferent(_editedColor, SelectedProject.Color))
            {
                DataHasChanged = true;
            }
            else
            {
                DataHasChanged = false;
            }
        }

        private bool IsDifferent(object first, object second)
        {
            if (first == null)
            {
                return false;
            }
            return !first.Equals(second);
        }

        public ProjectViewModel SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                SetPropertyField(ref _selectedProject, value);
                if (_selectedProject != null)
                {
                    EditedProjectName = _selectedProject.Name;
                    EditedColor = _selectedProject.Color;
                }
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