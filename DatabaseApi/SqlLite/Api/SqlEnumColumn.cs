using System;
using System.Linq;

namespace DatabaseApi.SqlLite.Api
{
    public class SqlEnumColumn : ISqlColumn
    {
        private readonly Type _enumType;
        public string Name { get; }
        public bool IsPrimaryKey { get; }
        public bool Nullable { get; }

        public SqlEnumColumn(string columnName, Type enumType)
        {
            _enumType = enumType;
            Name = columnName;
            IsPrimaryKey = false;
            Nullable = false;
        }

        public string BuildCreateQuery()
        {
            int longestEnum = _enumType.GetEnumNames().Max(s => s.Length) + 1;
            string nullable = Nullable ? "" : "NOT NULL";
            return $"{Name} varchar({longestEnum}) {nullable}";
        }


        public string EncapsulateValue(object value)
        {
            return $"'{value.ToString()}'";
        }

        public object ParseValue(object valueFromSql)
        {
            return Enum.Parse(_enumType, valueFromSql.ToString());
        }
    }
}