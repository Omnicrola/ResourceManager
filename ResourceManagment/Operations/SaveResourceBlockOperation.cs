using ResourceManagment.Data.Database;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Operations
{
    public class SaveResourceBlockOperation : AsyncDiscreetOperation
    {
        private readonly ResourceBlockViewModel _resourceBlock;
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public SaveResourceBlockOperation(ResourceBlockViewModel resourceBlock,
            ResourceManagerDatabaseSchema databaseSchema)
        {
            _resourceBlock = resourceBlock;
            _databaseSchema = databaseSchema;
        }

        public override string Description => "Saving resource block.";

        protected override void DoWorkInternal()
        {
            ResourceBlockModel resourceBlockModel = ConvertBlock(_resourceBlock);
            _databaseSchema.ResourceBlockTable.Save(resourceBlockModel);
        }

        private ResourceBlockModel ConvertBlock(ResourceBlockViewModel resourceBlockViewModel)
        {
            return new ResourceBlockModel()
            {
                Id = resourceBlockViewModel.Id,
                Date = resourceBlockViewModel.Date,
                PairPartnerId = resourceBlockViewModel.PairPartner?.ID,
                PersonId = resourceBlockViewModel.Person.ID.Value,
                ProjectId = resourceBlockViewModel.Project?.Id,
                WeeklyScheduleId = resourceBlockViewModel.ScheduleId
            };
        }
    }
}