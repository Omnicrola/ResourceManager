using System.Collections.Generic;
using System.Data.SQLite;
using DatabaseApi.SqlLite.Api;

namespace ResourceManagment.Data.Database.Tables
{
    public class ResourceBlockTable : SqlTable
    {
        public const string TableName = "resources";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlDateTimeColumn DateTime = new SqlDateTimeColumn("datetime");
        public static SqlIntegerColumn FkPerson = new SqlIntegerColumn("fk_person", false);
        public static SqlIntegerColumn FkProject = new SqlIntegerColumn("fk_project", false);
        public static SqlIntegerColumn FkSchedule = new SqlIntegerColumn("fk_schedule", false);

        public ResourceBlockTable()
        {

            Columns = new List<ISqlColumn>
            {
                Id,
                DateTime,
                FkPerson,
                FkProject
            };
            ForeignKeys.Add(new SqlForeignKey(FkPerson, PersonTable.TableName, PersonTable.Id));
            ForeignKeys.Add(new SqlForeignKey(FkProject, ProjectTable.TableName, ProjectTable.Id));
            ForeignKeys.Add(new SqlForeignKey(FkSchedule, WeeklyScheduleTable.TableName, WeeklyScheduleTable.Id));
        }

        public override string GetTableName()
        {
            return TableName;
        }
    }
}