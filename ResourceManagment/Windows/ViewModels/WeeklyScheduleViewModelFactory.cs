using System.Linq;
using DataApi.Extensions;
using DataApi.Models;
using ResourceManagment.Data;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.Main;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Windows.ViewModels
{
    internal class WeeklyScheduleViewModelFactory : IConversionFactory<IWeeklySchedule, WeekScheduleViewModel>
    {

        public WeekScheduleViewModel Build(IWeeklySchedule source)
        {
            return new WeekScheduleViewModel
            {
                Id = source.Id,
                WeekColor = source.WeekColor,
                WeekEnding = source.WeekEnding,
                Notes = source.Notes
            };
        }

    }
}