using System.Collections.Generic;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public class ResourceBlockTable : SqlTable
    {

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlDateTimeColumn DateTime = new SqlDateTimeColumn("datetime");
        public static SqlIntegerColumn FkPerson = new SqlIntegerColumn("fk_person", false);
        public static SqlIntegerColumn FkProject = new SqlIntegerColumn("fk_project", false);

        public ResourceBlockTable()
        {
            TableName = "resources";
            Columns = new List<ISqlColumn>
            {
                Id,
                DateTime,
                FkPerson,
                FkProject
            };
        }
    }

    public class SqlDateTimeColumn : ISqlColumn
    {
        private readonly string _name;

        public SqlDateTimeColumn(string name)
        {
            _name = name;
        }

        public string BuildCreateQuery()
        {
            return _name + " datetime NOT NULL";
        }
    }
}