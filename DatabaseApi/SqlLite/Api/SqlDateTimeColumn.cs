using System;

namespace DatabaseApi.SqlLite.Api
{
    public class SqlDateTimeColumn : ISqlColumn
    {

        public SqlDateTimeColumn(string name)
        {
            Name = name;
        }

        public string BuildCreateQuery()
        {
            string nullable = Nullable ? "" : "NOT NULL";
            return $"{Name} datetime {nullable}";
        }

        public string Name { get; }
        public bool IsPrimaryKey => false;
        public bool Nullable => false;

        public string EncapsulateValue(object value)
        {
            string formattedValue = value.ToString();
            if (value is DateTime)
            {
                var timestamp = ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss");
                formattedValue = $"'{timestamp}'";
            }
            return formattedValue;
        }

        public object ParseValue(object valueFromSql)
        {
            return valueFromSql;
        }
    }
}