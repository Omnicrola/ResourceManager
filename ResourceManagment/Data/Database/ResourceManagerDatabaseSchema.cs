using DatabaseApi.SqlLite;
using ResourceManagment.Data.Database.Tables;

namespace ResourceManagment.Data.Database
{
    public class ResourceManagerDatabaseSchema : DatabaseSchema
    {
        public ResourceManagerDatabaseSchema(string databaseLocation, SqlSchemaVerifier schemaVerifier)
            : base(databaseLocation, schemaVerifier)
        {
            ProjectTable = new ProjectTable(this);
            PersonTable = new PersonTable(this);
            WeeklyScheduleTable = new WeeklyScheduleTable(this);
            ResourceBlockTable = new ResourceBlockTable(this);

            SqlTables.Add(ProjectTable);
            SqlTables.Add(PersonTable);
            SqlTables.Add(WeeklyScheduleTable);
            SqlTables.Add(ResourceBlockTable);
        }

        public ResourceBlockTable ResourceBlockTable { get; }

        public WeeklyScheduleTable WeeklyScheduleTable { get; }

        public PersonTable PersonTable { get; }

        public ProjectTable ProjectTable { get; }
    }
}
