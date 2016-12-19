using DataApi.Extensions;
using DataApi.Models;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManageProjects;

namespace ResourceManagment.Windows.ViewModels
{
    internal class ProjectViewModelFactory : IConversionFactory<IProject, ProjectViewModel>
    {
        public ProjectViewModel Build(IProject source)
        {
            return new ProjectViewModel
            {
                Id = source.Id,
                Color = source.Color,
                Name = source.Name
            };
        }
    }
}