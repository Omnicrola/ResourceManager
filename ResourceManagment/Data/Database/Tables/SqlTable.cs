using System.Collections.Generic;
using System.Linq;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public abstract class SqlTable : ISqlTable
    {
        protected List<ISqlColumn> Columns;
        protected List<SqlForeignKey> ForeignKeys = new List<SqlForeignKey>();

        public string BuildCreateQuery()
        {
            string columns = Columns.Select(c => c.BuildCreateQuery())
                .Aggregate((total, next) => total + ", " + next);

            string foreignKeys = ForeignKeys.Select(f => f.BuildCreateQuery())
                .Aggregate((total, next) => total + " " + next);

            string creationQuery = "CREATE TABLE " + GetTableName() + " (" + columns + ");";
            return creationQuery;
        }

        public abstract string GetTableName();
    }
}