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
        private SolidColorBrush _color;
        private string _hexColor;
        public static ProjectViewModel Empty { get { return new ProjectViewModel("None") { Color = Brushes.Gray }; } }

        public ProjectViewModel(string name)
        {
            _name = name;
            Color = new SolidColorBrush(Colors.LightCoral);
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
        public string HexColor
        {
            get { return _hexColor; }
            set
            {
                _hexColor = value;

                var brushConverter = new BrushConverter();
                Color = (SolidColorBrush)brushConverter.ConvertFrom(_hexColor);
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                SaveHexValue(value);

                FireOnPropertyChanged("WeekColor");
            }
        }

        private void SaveHexValue(SolidColorBrush value)
        {
            var myColor = value.Color;
            _hexColor = "#" +
                        myColor.R.ToString("X2") +
                        myColor.G.ToString("X2") +
                        myColor.B.ToString("X2");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}