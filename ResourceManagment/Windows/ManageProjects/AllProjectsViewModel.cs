using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ResourceManagment.Windows
{
    public class AllProjectsViewModel : INotifyPropertyChanged
    {
        private string _editedProjectName;
        private ProjectViewModel _selectedProject;

        public AllProjectsViewModel(ObservableCollection<ProjectViewModel> projects)
        {
            Projects = projects;

        }
        public string EditedProjectName
        {
            get
            {
                return _editedProjectName;
            }
            set
            {
                _editedProjectName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EditedProjectName"));
            }
        }

        public ObservableCollection<ProjectViewModel> Projects { get; private set; }
        public ProjectViewModel SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;
                EditedProjectName = _selectedProject.Name;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedProject"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}