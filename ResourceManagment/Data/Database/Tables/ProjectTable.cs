using System.Collections.Generic;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public class ProjectTable : SqlTable
    {
        public const string TableName = "projects";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlStringColumn Name = new SqlStringColumn("name", 32);
        public static SqlStringColumn Color = new SqlStringColumn("color", 8);

        public ProjectTable()
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