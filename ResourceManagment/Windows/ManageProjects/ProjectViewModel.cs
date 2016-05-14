using System.Windows.Media;
using ResourceManagment.Data.Models;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageProjects
{
    public class ProjectViewModel : ViewModel, IProject
    {
        private string _name;
        private Brush _color;
        public static ProjectViewModel Empty { get { return new ProjectViewModel("None") { Color = Brushes.Gray }; } }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                FireOnPropertyChanged("Name");
            }
        }

        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                FireOnPropertyChanged("WeekColor");
            }
        }

        public ProjectViewModel(string name)
        {
            _name = name;
            Color = new SolidColorBrush(Colors.LightCoral);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}