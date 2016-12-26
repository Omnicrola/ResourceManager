using ResourceManagment.Data.Database;
using ResourceManagment.Windows.ManagePeople;

namespace ResourceManagment.Operations
{
    public class SavePersonOperation : AsyncDiscreetOperation
    {
        private readonly PersonViewModel _selectedPerson;
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public SavePersonOperation(PersonViewModel selectedPerson, ResourceManagerDatabaseSchema databaseSchema)
        {
            _selectedPerson = selectedPerson;
            _databaseSchema = databaseSchema;
        }

        public override string Description
        {
            get
            {
                var firstName = _selectedPerson.FirstName;
                var lastName = _selectedPerson.LastName;
                return $"Saving person : {firstName} {lastName}";
            }
        }

        protected override void DoWorkInternal()
        {
            //            _databaseSchema.PersonTable.Save(_selectedPerson);
        }
    }
}