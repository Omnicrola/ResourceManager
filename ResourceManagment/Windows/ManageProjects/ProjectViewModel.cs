using System.Windows.Media;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageProjects
{
    [SqlTableBinding("projects")]
    public class ProjectViewModel : ViewModel, IProject
    {
        private string _name;
        private Color _color;
        public static ProjectViewModel Empty { get { return new ProjectViewModel("None") { Color = Colors.LightGray }; } }

        public ProjectViewModel() { }
        public ProjectViewModel(string name)
        {
            _name = name;
            Color = Colors.LightGray;
        }

        [SqlColumnBinding("id")]
        public int? Id { get; set; }

        [SqlColumnBinding("name")]
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

        [SqlColumnBinding("color")]
        public Color Color
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

        public override string ToString()
        {
            return Name;
        }
    }
}