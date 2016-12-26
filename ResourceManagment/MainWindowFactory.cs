using System.Collections.Specialized;
using System.Linq;
using System.Windows.Threading;
using DataApi.Api;
using DataApi.Extensions;
using ResourceManagment.Data;
using ResourceManagment.Data.Model;
using ResourceManagment.Operations;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.Main;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment
{
    public class MainWindowFactory
    {
        public static MainWindow Build(IDataRepository dataRepository, Dispatcher dispatcher)
        {
            var operationsQueue = new OperationsQueue(dispatcher);

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.People.SlaveTo(dataRepository.AllPeople, new PeopleViewModelFactory());
            mainWindowViewModel.Projects.SlaveTo(dataRepository.AllProjects, new ProjectViewModelFactory());
            mainWindowViewModel.AllSchedules.SlaveTo(dataRepository.AllWeeklySchedules, new WeeklyScheduleViewModelFactory());

            dataRepository.AllResourceBlocks.DataChanged += OnResourceBlockChange(mainWindowViewModel);

            var userOperationsFactory = new UserOperationsBuilder(operationsQueue, null);

            return new MainWindow(mainWindowViewModel, userOperationsFactory);
        }

        private static NotifyCollectionChangedEventHandler OnResourceBlockChange(MainWindowViewModel mainWindowViewModel)
        {
            return (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var newItem in args.NewItems)
                    {
                        MapResourcesToSchedules(mainWindowViewModel, (ResourceBlockModel)newItem);
                    }
                }
            };

        }

        private static void MapResourcesToSchedules(MainWindowViewModel mainWindowViewModel, ResourceBlockModel resourceBlockModel)
        {
            var person = mainWindowViewModel.People.First(p => p.ID == resourceBlockModel.PersonId);
            var pairPartner = mainWindowViewModel.People.First(p => p.ID == resourceBlockModel.PairPartnerId);
            var project = mainWindowViewModel.Projects.First(p => p.Id == resourceBlockModel.ProjectId);
            var weeklySchedule = mainWindowViewModel.AllSchedules.First(s => s.Id == resourceBlockModel.WeeklyScheduleId);

            var resourceBlockViewModel = new ResourceBlockViewModel(person, resourceBlockModel.BlockOrder)
            {
                Project = project,
                PairPartner = pairPartner
            };
            weeklySchedule.OverwriteBlock(resourceBlockViewModel);
        }
    }
}