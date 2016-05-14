using DatabaseApi.SqlLite;
using ResourceManagment.Data.Database.Tables;

namespace ResourceManagment.Data.Database
{
    public class ResourceManagerDatabaseSchema : DatabaseSchema
    {

        public ResourceManagerDatabaseSchema() : base()
        {
            SqlTables.Add(new ProjectTable());
            SqlTables.Add(new PersonTable());
            SqlTables.Add(new WeeklyScheduleTable());
            SqlTables.Add(new ResourceBlockTable());

        }
    }
}
