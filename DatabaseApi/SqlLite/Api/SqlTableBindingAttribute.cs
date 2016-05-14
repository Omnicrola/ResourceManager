using System;

namespace DatabaseApi.SqlLite.Api
{
    public class SqlTableBindingAttribute : Attribute
    {
        public string Tablename { get; }

        public SqlTableBindingAttribute(string tablename)
        {
            Tablename = tablename;
        }
    }
}