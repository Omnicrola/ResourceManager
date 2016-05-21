using System.Collections.Generic;
using ResourceManagment.Data.Database;
using ResourceManagment.Windows.Main;
using ResourceManagment.Windows.ManagePeople;

namespace ResourceManagment.Operations
{
    public class LoadInitialDataOperation : AsyncDiscreetOperation
    {
        private readonly ResourceManagerDatabaseSchema _databaseSchema;
        private List<PersonViewModel> _personViewModels;

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
            _personViewModels = _databaseSchema.PersonTable.GetAll<PersonViewModel>();
        }

        public void Populate(MainWindowViewModel mainWindowViewModel)
        {
            _personViewModels.ForEach(p => mainWindowViewModel.People.Add(p));
        }
    }
}