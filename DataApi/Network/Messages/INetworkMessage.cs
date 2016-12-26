using DataApi.Api;

namespace DataApi.Network.Messages
{
    public interface INetworkMessage
    {
        void Resolve(IDataRepositoryWrite networkDataRepository);
    }
}