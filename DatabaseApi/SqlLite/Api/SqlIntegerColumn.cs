using System;

namespace DatabaseApi.SqlLite.Api
{
    public class SqlIntegerColumn : ISqlColumn
    {

        public SqlIntegerColumn(string name, bool isPrimaryKey, bool nullable)
        {
            Name = name;
            IsPrimaryKey = isPrimaryKey;
            Nullable = nullable;
        }

        public string BuildCreateQuery()
        {
            string pk = IsPrimaryKey ? "PRIMARY KEY AUTOINCREMENT" : "";
            string nullable = Nullable ? "" : "NOT NULL";
            return $"{Name} INTEGER {pk} {nullable}";
        }

        public string Name { get; }
        public bool IsPrimaryKey { get; }
        public bool Nullable { get; }

        public string EncapsulateValue(object value)
        {
            if (value == null)
            {
                return "NULL";
            }
            return $"'{value.ToString()}'";
        }

        public object ParseValue(object valueFromSql)
        {
            return Convert.ToInt32(valueFromSql);
        }
    }
}