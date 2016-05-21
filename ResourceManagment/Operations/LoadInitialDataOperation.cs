using ResourceManagment.Data.Database;
using ResourceManagment.Windows.Main;

namespace ResourceManagment.Operations
{
    public class LoadInitialDataOperation : AsyncDiscreetOperation
    {
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public LoadInitialDataOperation(ResourceManagerDatabaseSchema databaseSchema)
        {
            _databaseSchema = databaseSchema;
        }

        public override string Description
        {
            get { return "Performing first loading of existing data."; }
        }

        protected override void DoWorkInternal()
        {
            _databaseSchema.PersonTable.GetAll();
        }

        public void Populate(MainWindowViewModel mainWindowViewModel)
        {

        }
    }
}