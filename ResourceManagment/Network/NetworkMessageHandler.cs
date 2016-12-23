using DataApi.Api;
using DataApi.Network.Messages;
using DataApi.Network.Wrapper;

namespace ResourceManagment.Network
{
    internal class NetworkMessageHandler
    {
        private readonly INetworkManager _networkManager;
        private IDataRepositoryWrite _dataRepository;

        public NetworkMessageHandler(INetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void LoadPeople()
        {
            _networkManager.SendMessage(new GetPeopleMessage());
        }

        public void RegisterRepository(IDataRepositoryWrite dataRepository)
        {
            _dataRepository = dataRepository;
            _networkManager.AddMessageHandler(GetPeopleCallback);
        }

        private void GetPeopleCallback(NetworkCommsDotNet.PacketHeader packetheader, NetworkCommsDotNet.Connections.Connection connection, object incomingobject)
        {

        }
    }
}