using System.Collections.Generic;
using DatabaseApi.SqlLite.Api;

namespace DatabaseApi.SqlLite
{
    public class MetadataTable : SqlTable
    {
        public const string TableName = "metadata";
        public static SqlStringColumn Key = new SqlStringColumn("key", 16);
        public static SqlStringColumn Value = new SqlStringColumn("value", 16);

        public MetadataTable()
        {
            Columns = new List<ISqlColumn>
            {
                Key,
                Value
            };
        }

        public override string GetTableName()
        {
            return TableName;
        }
    }
}
