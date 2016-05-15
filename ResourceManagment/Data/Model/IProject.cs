using System.ComponentModel;
using System.Windows.Media;

namespace ResourceManagment.Data.Model
{
    public interface IProject : INotifyPropertyChanged
    {
        Brush Color { get; set; }
        string Name { get; set; }
    }
}