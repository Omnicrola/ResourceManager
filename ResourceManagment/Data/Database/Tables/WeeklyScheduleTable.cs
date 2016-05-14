﻿using System.Collections.Generic;
using DatabaseApi.SqlLite;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Data.Database.Tables
{
    public class WeeklyScheduleTable : SqlTable<IWeeklySchedule>
    {
        public const string TableName = "weekly_schedules";
        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlDateTimeColumn WeekEnding = new SqlDateTimeColumn("week_ending");
        public static SqlStringColumn Color = new SqlStringColumn("color", 8);

        public WeeklyScheduleTable(DatabaseSchema databaseSchema) : base(databaseSchema)
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