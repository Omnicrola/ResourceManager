using System.ComponentModel;
using System.Windows.Media;

namespace ResourceManagment.Data.Model
{
    public interface IProject
    {
        Color Color { get; set; }
        string Name { get; set; }
        int? Id { get; set; }
    }
}