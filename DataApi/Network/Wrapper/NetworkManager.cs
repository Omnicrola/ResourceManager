using System;
using DataApi.Network.Messages;
using NetworkCommsDotNet;

namespace DataApi.Network.Wrapper
{
    public class NetworkManager : INetworkManager
    {
        private readonly NetworkConfiguration _networkConfiguration;

        public NetworkManager(NetworkConfiguration networkConfiguration)
        {
            _networkConfiguration = networkConfiguration;
        }

        public void SendMessage<T>(T message) where T : INetworkMessage
        {
            try
            {
                NetworkComms.SendObject(message.GetType().Name,
                    _networkConfiguration.Address,
                    _networkConfiguration.Port,
                    message);
            }
            catch (CommsException e)
            {
                Console.WriteLine("Network Exception: " + e.Message);
            }
        }

        public void AddMessageHandler<T>(NetworkComms.PacketHandlerCallBackDelegate<T> callback)
        {
            try
            {
                NetworkComms.AppendGlobalIncomingPacketHandler(typeof(T).Name, callback);
            }
            catch (CommsException e)
            {
                Console.WriteLine("Network Exception : " + e.Message);
            }
        }
    }
}