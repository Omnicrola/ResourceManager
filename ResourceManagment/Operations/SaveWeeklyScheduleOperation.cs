using ResourceManagment.Data.Database;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Operations
{
    public class SaveWeeklyScheduleOperation : AsyncDiscreetOperation
    {
        private readonly WeekScheduleViewModel _weekScheduleViewModel;
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public SaveWeeklyScheduleOperation(WeekScheduleViewModel weekScheduleViewModel, ResourceManagerDatabaseSchema databaseSchema)
        {
            _weekScheduleViewModel = weekScheduleViewModel;
            _databaseSchema = databaseSchema;
        }

        public override string Description => $"Saving weekly schedule : {_weekScheduleViewModel.WeekEnding}";

        protected override void DoWorkInternal()
        {
            _databaseSchema.WeeklyScheduleTable.Save(_weekScheduleViewModel);
        }
    }
}