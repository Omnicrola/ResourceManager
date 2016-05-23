using System;
using System.Collections.Generic;
using System.Linq;
using ResourceManagment.Data.Database;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Operations
{
    public class SavePersonalScheduleOperation : AsyncDiscreetOperation
    {
        private readonly WeekScheduleViewModel _selectedSchedule;
        private readonly PersonalScheduleViewModel _personalSchedule;
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public SavePersonalScheduleOperation(WeekScheduleViewModel selectedSchedule,
            PersonalScheduleViewModel personalSchedule,
            ResourceManagerDatabaseSchema databaseSchema)
        {
            _selectedSchedule = selectedSchedule;
            _personalSchedule = personalSchedule;
            _databaseSchema = databaseSchema;
        }

        public override string Description => $"Saving schedule for on {_personalSchedule.Friday.Date} " +
                                              $"for{_personalSchedule.Person.FirstName} {_personalSchedule.Person.LastName}";

        protected override void DoWorkInternal()
        {
            var allDays = new List<IResourceBlock>();
            foreach (var workDayViewModel in _personalSchedule.Days)
            {
                var morning = workDayViewModel.Morning.ConvertToSqlModel(_selectedSchedule.Id.Value);
                var afternoon = workDayViewModel.Afternoon.ConvertToSqlModel(_selectedSchedule.Id.Value);

                allDays.Add(morning);
                allDays.Add(afternoon);
            }
            _databaseSchema.ResourceBlockTable.SaveAll(allDays);
        }

    }
}