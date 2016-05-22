using System;
using ResourceManagment.Data.Database;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Operations
{
    public class UserOperationsBuilder : IDisposable
    {
        private readonly OperationsQueue _operationsQueue;
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public UserOperationsBuilder(OperationsQueue operationsQueue, ResourceManagerDatabaseSchema databaseSchema)
        {
            _operationsQueue = operationsQueue;
            _databaseSchema = databaseSchema;
        }

        public void Dispose()
        {
            _operationsQueue.Dispose();
        }

        public void SavePerson(PersonViewModel selectedPerson)
        {
            var savePersonOperation = new SavePersonOperation(selectedPerson, _databaseSchema);
            _operationsQueue.AddOperation(savePersonOperation);
        }

        public void SaveProject(ProjectViewModel selectedProject)
        {
            var saveProjectOperation = new SaveProjectOperation(selectedProject, _databaseSchema);
            _operationsQueue.AddOperation(saveProjectOperation);
        }

        public void SaveWeeklySchedule(WeekScheduleViewModel weekScheduleViewModel)
        {
            var saveWeeklyScheduleOperation = new SaveWeeklyScheduleOperation(weekScheduleViewModel, _databaseSchema);
            _operationsQueue.AddOperation(saveWeeklyScheduleOperation);
        }

        public void SavePersonalSchedule(PersonalScheduleViewModel personalSchedule, WeekScheduleViewModel selectedSchedule)
        {
            var savePersonalScheduleOperation = new SavePersonalScheduleOperation(selectedSchedule, personalSchedule, _databaseSchema);
            _operationsQueue.AddOperation(savePersonalScheduleOperation);
        }

        public void SaveResourceBlock(ResourceBlockViewModel resourceBlock)
        {
            var saveResourceBlockOperation = new SaveResourceBlockOperation(resourceBlock, _databaseSchema);
            _operationsQueue.AddOperation(saveResourceBlockOperation);
        }
    }
}