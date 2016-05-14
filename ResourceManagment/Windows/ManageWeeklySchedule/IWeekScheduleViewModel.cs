using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using ResourceManagment.Data.Models;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    public interface IWeekScheduleViewModel : IWeeklySchedule
    {
        string Notes { get; set; }
        ObservableCollection<PersonalScheduleViewModel> Schedules { get; set; }
        ObservableCollection<RequiredResourceViewModel> RequiredProjectResources { get; set; }
        DateTime WeekEnding { get; set; }
        Color WeekColor { get; set; }

        void Save();

    }
}