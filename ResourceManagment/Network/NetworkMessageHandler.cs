using System.Collections.Generic;
using DataApi.Api;
using DataApi.Network.Messages;
using DataApi.Network.Messages.Response;
using DataApi.Network.Wrapper;
using NetworkCommsDotNet;

namespace ResourceManagment.Network
{
    internal class NetworkMessageHandler : INetworkMessageQueue
    {
        private readonly INetworkManager _networkManager;
        private IDataRepositoryWrite _dataRepository;
        private readonly Queue<INetworkMessage> _incomingMessages = new Queue<INetworkMessage>();

        private readonly object MESSAGE_LOCK = new object();

        public NetworkMessageHandler(INetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void LoadPeople()
        {
            _networkManager.SendMessage(new GetPeopleResponse());
        }

        public void RegisterRepository(IDataRepositoryWrite dataRepository)
        {
            _dataRepository = dataRepository;
            RegisterListener<GetPeopleResponse>();
        }

        private void RegisterListener<T>() where T : INetworkMessage
        {
            _networkManager.AddMessageHandler<T>((p, h, m) =>
            {
                lock (MESSAGE_LOCK)
                {
                    _incomingMessages.Enqueue((INetworkMessage)m);
                }
            });
        }

        public bool HasIncomingMessages()
        {
            return _incomingMessages.Count > 0;
        }

        public INetworkMessage GetNextIncomingMessage()
        {
            lock (MESSAGE_LOCK)
            {
                return _incomingMessages.Dequeue();
            }
        }
    }
}