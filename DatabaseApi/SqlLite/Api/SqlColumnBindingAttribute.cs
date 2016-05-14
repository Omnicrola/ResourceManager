using System;

namespace DatabaseApi.SqlLite.Api
{
    public class SqlColumnBindingAttribute : Attribute
    {
        public string ColumnName { get; }

        public SqlColumnBindingAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}