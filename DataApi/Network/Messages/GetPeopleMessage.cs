using System.Collections.Generic;
using DataApi.Models;
using ProtoBuf;

namespace DataApi.Network.Messages
{
    [ProtoContract]
    public class GetPeopleMessage : INetworkMessage
    {
        [ProtoMember(1)]
        public List<IPerson> Person { get; set; }
    }
}