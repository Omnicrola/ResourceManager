using System.Windows.Media;

namespace DataApi.Models
{
    public interface IProject
    {
        Color Color { get; set; }
        string Name { get; set; }
        int? Id { get; set; }
    }
}