using DataApi.Models;

namespace ResourceManagment.Network.Models
{
    public class NetworkPerson : IPerson
    {
        public int? ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }

        public void Apply(IPerson person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Role = person.Role;
        }
    }
}