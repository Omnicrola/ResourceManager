using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using DataApi.Api;
using DataApi.Models;
using DataApi.Network.Wrapper;
using ResourceManagment.Data;

namespace ResourceManagment.Network
{
    public class NetworkDataRepository : IDataRepositoryWrite
    {
        private readonly ObservableCollection<IPerson> _allPeople;
        private readonly ObservableCollection<IProject> _allProjects;
        private readonly ObservableCollection<IWeeklySchedule> _allWeeklySchedules;
        private readonly ObservableCollection<IResourceBlock> _allResourceBlocks;

        public DataCollection<IPerson> AllPeople { get; }
        public DataCollection<IProject> AllProjects { get; }
        public DataCollection<IWeeklySchedule> AllWeeklySchedules { get; }
        public DataCollection<IResourceBlock> AllResourceBlocks { get; }

        private static IDataRepository _instance;

        public static IDataRepository Instance()
        {
            if (_instance == null)
            {
                string serverAddress = ConfigurationManager.AppSettings["ServerAddress"];
                int serverPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);

                var networkManager = new NetworkManager(new NetworkConfiguration(serverAddress, serverPort));
                NetworkMessageHandler messageHandler = new NetworkMessageHandler(networkManager);
                _instance = new NetworkDataRepository(messageHandler);
            }
            return _instance;
        }

        private NetworkDataRepository(NetworkMessageHandler networkMessageHandler)
        {
            _allPeople = new ObservableCollection<IPerson>();
            AllPeople = new DataCollection<IPerson>(_allPeople);

            _allProjects = new ObservableCollection<IProject>();
            AllProjects = new DataCollection<IProject>(_allProjects);

            _allWeeklySchedules = new ObservableCollection<IWeeklySchedule>();
            AllWeeklySchedules = new DataCollection<IWeeklySchedule>(_allWeeklySchedules);

            _allResourceBlocks = new ObservableCollection<IResourceBlock>();
            AllResourceBlocks = new DataCollection<IResourceBlock>(_allResourceBlocks);

            networkMessageHandler.LoadPeople();
            networkMessageHandler.RegisterRepository(this);
        }

    }
}