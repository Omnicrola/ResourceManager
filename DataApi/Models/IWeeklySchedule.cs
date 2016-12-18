using System;
using System.ComponentModel;
using System.Windows.Media;

namespace ResourceManagment.Data.Model
{
    public interface IWeeklySchedule
    {
        DateTime WeekEnding { get; set; }
        Color WeekColor { get; set; }
        string Notes { get; set; }
    }
}