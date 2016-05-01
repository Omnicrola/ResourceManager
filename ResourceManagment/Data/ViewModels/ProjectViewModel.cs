using BusinessLogic.Models;

namespace ResourceManagment
{
    public class ProjectViewModel : ViewModel
    {
        private Project _project;

        public string Name
        {
            get
            {
                return _project.Name;
            }
            set
            {
                _project.Name = value;
                FireOnPropertyChanged("Name");
            }
        }

        public ProjectViewModel(Project project)
        {
            _project = project;
        }
    }
}