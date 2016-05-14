using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace DatabaseApi.SqlLite.Api
{
    public abstract class SqlTable : ISqlTable
    {
        public SQLiteConnection Connection { get; set; }
        protected List<ISqlColumn> Columns;
        protected List<SqlForeignKey> ForeignKeys = new List<SqlForeignKey>();
        protected DatabaseSchema DatabaseSchema { get; }

        protected SqlTable(DatabaseSchema databaseSchema)
        {
            DatabaseSchema = databaseSchema;
        }

        public string BuildCreateQuery()
        {
            string columns = Columns.Select(c => c.BuildCreateQuery())
                .Aggregate((total, next) => total + ", " + next);

            string foreignKeys = ForeignKeys.Select(f => f.BuildCreateQuery())
                .Aggregate((total, next) => total + ", " + next);

            string creationQuery = $"CREATE TABLE {GetTableName()}  ({columns}, {foreignKeys});";
            return creationQuery;
        }

        public abstract string GetTableName();
    }
}