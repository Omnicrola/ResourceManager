using System;
using System.Windows.Threading;
using DataApi.Api;

namespace ResourceManagment.Network
{
    internal class ClientNetworkMessageProcessor
    {
        private readonly Dispatcher _dispatcher;
        private readonly INetworkMessageQueue _networkMessageHandler;
        private readonly IDataRepositoryWrite _networkDataRepository;

        public ClientNetworkMessageProcessor(Dispatcher dispatcher, INetworkMessageQueue networkMessageHandler, IDataRepositoryWrite networkDataRepository)
        {
            _dispatcher = dispatcher;
            _networkMessageHandler = networkMessageHandler;
            _networkDataRepository = networkDataRepository;
        }

        public void ProcessMessageQueue()
        {
            if (_networkMessageHandler.HasIncomingMessages())
            {
                var nextIncomingMessage = _networkMessageHandler.GetNextIncomingMessage();
                nextIncomingMessage.Resolve(_networkDataRepository);
                _dispatcher.BeginInvoke((Action)this.ProcessMessageQueue);
            }
        }
    }
}