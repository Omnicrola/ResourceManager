using System.Collections.ObjectModel;
using System.Windows;

namespace ResourceManagment.Windows
{
    /// <summary>
    /// Interaction logic for ManageProjects.xaml
    /// </summary>
    public partial class ManageProjectsWindow : Window
    {
        private AllProjectsViewModel _allProjectsViewModel;

        public ManageProjectsWindow(AllProjectsViewModel projectDataContext)
        {
            InitializeComponent();
            DataContext = projectDataContext;
            _allProjectsViewModel = projectDataContext;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            _allProjectsViewModel.SelectedProject.Name = _allProjectsViewModel.EditedProjectName;
        }

        private void ButtonNewProject_Click(object sender, RoutedEventArgs e)
        {
            var newProject = new ProjectViewModel("New Project");
            _allProjectsViewModel.Projects.Add(newProject);
            _allProjectsViewModel.SelectedProject = newProject;
        }
    }
}
