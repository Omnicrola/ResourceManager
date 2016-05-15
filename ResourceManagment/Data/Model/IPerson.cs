using ResourceManagment.Data.Filtering.ResourceFilters;

namespace ResourceManagment.Data.Model
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        Role Role { get; set; }
    }
}