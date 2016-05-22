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
                var morning = ConvertModel(workDayViewModel.Morning);
                var afternoon = ConvertModel(workDayViewModel.Afternoon);

                allDays.Add(morning);
                allDays.Add(afternoon);
            }
            _databaseSchema.ResourceBlockTable.SaveAll(allDays);
        }

        private ResourceBlockModel ConvertModel(ResourceBlockViewModel resourceBlock)
        {
            return new ResourceBlockModel()
            {
                Id = resourceBlock.Id,
                PairPartnerId = resourceBlock.PairPartner?.ID,
                PersonId = resourceBlock.Person.ID.Value,
                WeeklyScheduleId = _selectedSchedule.Id,
                ProjectId = resourceBlock.Project.Id,
                Date = resourceBlock.Date
            };
        }
    }
}