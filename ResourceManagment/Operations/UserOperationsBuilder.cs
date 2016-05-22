using System;
using ResourceManagment.Data.Database;
using ResourceManagment.Windows.ManagePeople;

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
            var savePersonCommand = new SavePersonOperation(selectedPerson, _databaseSchema);
            _operationsQueue.AddOperation(savePersonCommand);
        }
    }
}