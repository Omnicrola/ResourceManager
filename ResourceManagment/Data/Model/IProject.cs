using System.ComponentModel;
using System.Windows.Media;

namespace ResourceManagment.Data.Model
{
    public interface IProject : INotifyPropertyChanged
    {
        SolidColorBrush Color { get; set; }
        string Name { get; set; }
    }
}