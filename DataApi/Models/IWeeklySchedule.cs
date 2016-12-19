using System;
using System.Windows.Media;

namespace DataApi.Models
{
    public interface IWeeklySchedule
    {
        int? Id { get; set; }
        DateTime WeekEnding { get; set; }
        Color WeekColor { get; set; }
        string Notes { get; set; }
    }
}