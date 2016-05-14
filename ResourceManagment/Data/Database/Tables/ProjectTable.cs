using System.Collections.Generic;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public class ProjectTable : SqlTable
    {
        public static SqlIntegerColumn ID = new SqlIntegerColumn("id", true);
        public static SqlStringColumn Name = new SqlStringColumn("name", 32);
        public static SqlStringColumn Color = new SqlStringColumn("color", 8);

        public ProjectTable()
        {
            TableName = "projects";
            Columns = new List<ISqlColumn>
            {
                ID,
                Name,
                Color
            };
        }
    }
}