using System.Collections.Generic;
using System.Linq;
using DatabaseApi.SqlLite.Api;

namespace DatabaseApi.SqlLite
{
    public abstract class DatabaseSchema
    {
        protected readonly List<ISqlTable> SqlTables = new List<ISqlTable>();

        protected DatabaseSchema()
        {
            SqlTables.Add(new MetadataTable());
        }

        public string BuildCreateQuery()
        {
            return
                SqlTables.Select(t => t.BuildCreateQuery())
                    .Aggregate((cumulative, current) => cumulative + "; " + current);
        }
    }
}