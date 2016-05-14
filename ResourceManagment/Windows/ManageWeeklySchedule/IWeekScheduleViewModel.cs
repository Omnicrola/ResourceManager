using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    public interface IWeekScheduleViewModel
    {
        string Notes { get; set; }
        ObservableCollection<PersonalScheduleViewModel> Schedules { get; set; }
        ObservableCollection<RequiredResourceViewModel> RequiredProjectResources { get; set; }
        DateTime WeekEnding { get; set; }
        Color WeekColor { get; set; }

        void Save();

    }
}