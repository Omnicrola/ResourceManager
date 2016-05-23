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
        private readonly int _scheduleId;

        public SaveResourceBlockOperation(ResourceBlockViewModel resourceBlock,
            ResourceManagerDatabaseSchema databaseSchema, int scheduleId)
        {
            _resourceBlock = resourceBlock;
            _databaseSchema = databaseSchema;
            _scheduleId = scheduleId;
        }

        public override string Description => "Saving resource block.";

        protected override void DoWorkInternal()
        {
            ResourceBlockModel resourceBlockModel = _resourceBlock.ConvertToSqlModel(_scheduleId);
            _databaseSchema.ResourceBlockTable.Save(resourceBlockModel);
        }

    }
}