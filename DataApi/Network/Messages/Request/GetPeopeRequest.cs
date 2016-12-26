using DataApi.Api;
using ProtoBuf;

namespace DataApi.Network.Messages.Request
{
    [ProtoContract]
    public class GetPeopeRequest : INetworkMessage
    {
        public void Resolve(IDataRepositoryWrite networkDataRepository)
        {

        }
    }
}