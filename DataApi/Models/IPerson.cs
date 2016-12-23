using ProtoBuf;

namespace DataApi.Models
{
    [ProtoContract]
    public interface IPerson
    {
        [ProtoMember(1)]
        int? ID { get; set; }

        [ProtoMember(2)]
        string FirstName { get; set; }

        [ProtoMember(3)]
        string LastName { get; set; }

        [ProtoMember(4)]
        Role Role { get; set; }
    }
}