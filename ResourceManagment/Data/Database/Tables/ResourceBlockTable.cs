using System.Collections.Generic;
using DatabaseApi.SqlLite;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.AlterResourceBlock;

namespace ResourceManagment.Data.Database.Tables
{
    public class ResourceBlockTable : SqlTable<IResourceBlock>
    {
        public const string TableName = "resources";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlDateTimeColumn DateTime = new SqlDateTimeColumn("datetime");
        public static SqlIntegerColumn FkPerson = new SqlIntegerColumn("fk_person", false);
        public static SqlIntegerColumn FkProject = new SqlIntegerColumn("fk_project", false);
        public static SqlIntegerColumn FkSchedule = new SqlIntegerColumn("fk_schedule", false);

        public ResourceBlockTable(DatabaseSchema databaseSchema) : base(databaseSchema)
        {

            Columns = new List<ISqlColumn>
            {
                Id,
                DateTime,
                FkPerson,
                FkProject,
                FkSchedule
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