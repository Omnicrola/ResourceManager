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
            return Name + " datetime NOT NULL";
        }

        public string Name { get; }
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
    }
}