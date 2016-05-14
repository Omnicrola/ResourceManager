using System.Collections.Generic;
using DatabaseApi.SqlLite;
using DatabaseApi.SqlLite.Api;

namespace ResourceManagment.Data.Database.Tables
{
    public class ProjectTable : SqlTable
    {
        public const string TableName = "projects";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlStringColumn Name = new SqlStringColumn("name", 32);
        public static SqlStringColumn Color = new SqlStringColumn("color", 8);
        private ResourceManagerDatabaseSchema resourceManagerDatabaseSchema;

        public ProjectTable(DatabaseSchema databaseSchema) : base(databaseSchema)
        {
            Columns = new List<ISqlColumn>
            {
                Id,
                Name,
                Color
            };
        }


        public override string GetTableName()
        {
            return TableName;
        }
    }
}