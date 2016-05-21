using System.ComponentModel;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Filtering.ResourceFilters;

namespace ResourceManagment.Data.Model
{
    [SqlTableBinding("people")]
    public interface IPerson : INotifyPropertyChanged
    {
        [SqlColumnBinding("id")]
        int ID { get; set; }

        [SqlColumnBinding("first_name")]
        string FirstName { get; set; }

        [SqlColumnBinding("last_name")]
        string LastName { get; set; }

        [SqlColumnBinding("role")]
        Role Role { get; set; }
    }
}