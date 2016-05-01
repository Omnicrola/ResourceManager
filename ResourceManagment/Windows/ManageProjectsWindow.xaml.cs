using System.Collections.ObjectModel;
using System.Windows;

namespace ResourceManagment.Windows
{
    /// <summary>
    /// Interaction logic for ManageProjects.xaml
    /// </summary>
    public partial class ManageProjectsWindow : Window
    {
        private ObservableCollection<ProjectViewModel> Projects { get; set; }

        public ManageProjectsWindow(ObservableCollection<ProjectViewModel> projects)
        {
            Projects = projects;
            InitializeComponent();
            DataContext = this;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string projectName = textboxProjectName.Text;
            Projects.Add(new ProjectViewModel(projectName));
        }
    }
}
