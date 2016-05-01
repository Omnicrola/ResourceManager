using System.Windows.Media;

namespace ResourceManagment
{
    public class ProjectViewModel : ViewModel
    {
        private string _name;
        private Brush _color;

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
                FireOnPropertyChanged("Color");
            }
        }

        public ProjectViewModel(string name)
        {
            _name = name;
            Color = new SolidColorBrush(Colors.AliceBlue);
        }
    }
}