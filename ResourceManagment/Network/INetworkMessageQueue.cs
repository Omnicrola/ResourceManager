using DataApi.Network.Messages;

namespace ResourceManagment.Network
{
    internal interface INetworkMessageQueue
    {
        bool HasIncomingMessages();
        INetworkMessage GetNextIncomingMessage();
    }
}