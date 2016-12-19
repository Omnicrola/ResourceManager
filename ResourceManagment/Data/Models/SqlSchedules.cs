using System;
using System.Windows.Media;
using DataApi.Models;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;

namespace ResourceManagment.Data
{
    [SqlTableBinding("weekly_schedules")]

    internal class SqlSchedules : IWeeklySchedule
    {
        [SqlColumnBinding("id")]
        public int? Id { get; set; }

        [SqlColumnBinding("week_ending")]
        public DateTime WeekEnding { get; set; }

        [SqlColumnBinding("color")]
        public Color WeekColor { get; set; }

        public string Notes { get; set; }
    }
}