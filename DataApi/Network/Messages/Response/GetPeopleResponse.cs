using System.Collections.Generic;
using DataApi.Api;
using DataApi.Models;
using ProtoBuf;

namespace DataApi.Network.Messages.Response
{
    [ProtoContract]
    public class GetPeopleResponse : INetworkMessage
    {
        [ProtoMember(1)]
        public List<IPerson> People { get; set; }

        public void Resolve(IDataRepositoryWrite networkDataRepository)
        {
            foreach (IPerson person in People)
            {
                networkDataRepository.PutPerson(person);
            }
        }
    }
}