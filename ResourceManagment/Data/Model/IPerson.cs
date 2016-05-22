using System.ComponentModel;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Filtering.ResourceFilters;

namespace ResourceManagment.Data.Model
{

    public interface IPerson : INotifyPropertyChanged
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        Role Role { get; set; }
    }
}