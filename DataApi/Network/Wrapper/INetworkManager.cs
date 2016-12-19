using DataApi.Network.Messages;
using NetworkCommsDotNet;

namespace DataApi.Network.Wrapper
{
    public interface INetworkManager
    {
        void AddMessageHandler<T>(NetworkComms.PacketHandlerCallBackDelegate<T> callback);
        void SendMessage<T>(T message) where T : INetworkMessage;
    }
}