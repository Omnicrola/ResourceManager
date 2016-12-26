using ProtoBuf;

namespace DataApi.Models
{
    public interface IPerson
    {
        int? ID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Role Role { get; set; }

        void Apply(IPerson person);
    }
}