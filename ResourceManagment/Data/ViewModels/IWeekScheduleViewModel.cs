using System;
using System.Collections.ObjectModel;

namespace ResourceManagment.Data.ViewModels
{
    public interface IWeekScheduleViewModel
    {
        string Notes { get; set; }
        ObservableCollection<PersonalScheduleViewModel> Schedules { get; set; }
        ObservableCollection<RequiredResourceViewModel> RequiredProjectResources { get; set; }
        DateTime WeekEnding { get; set; }
        System.Windows.Media.Color WeekColor { get; set; }

        void Save();

    }
}