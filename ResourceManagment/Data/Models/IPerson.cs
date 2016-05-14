using ResourceManagment.Data.Filtering.ResourceFilters;

namespace ResourceManagment.Data.Models
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        Role Role { get; set; }
    }
}