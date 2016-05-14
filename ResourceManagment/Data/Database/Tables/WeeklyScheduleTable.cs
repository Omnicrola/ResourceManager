using System.Collections.Generic;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public class WeeklyScheduleTable : SqlTable
    {
        public const string TableName = "weekly_schedules";
        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlDateTimeColumn WeekEnding = new SqlDateTimeColumn("week_ending");
        public static SqlStringColumn Color = new SqlStringColumn("color", 8);

        public WeeklyScheduleTable()
        {
            Columns = new List<ISqlColumn>
            {
                Id,
                WeekEnding,
                Color
            };
        }

        public override string GetTableName()
        {
            return TableName;
        }
    }
}