using System.Collections.Generic;
using System.Linq;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public abstract class SqlTable : ISqlTable
    {
        public string TableName { get; protected set; }
        protected List<ISqlColumn> Columns;

        public string BuildCreateQuery()
        {
            string columns = Columns.Select(c => c.BuildCreateQuery())
                .Aggregate((total, next) => total + ", " + next);
            string creationQuery = "CREATE TABLE " + TableName + " (" + columns + ");";
            return creationQuery;
        }
    }
}