using System.Reflection;

namespace DatabaseApi.SqlLite.Api
{
    internal class SqlColumnBinding
    {
        public SqlColumnBindingAttribute BindingAttribute { get; }
        public PropertyInfo PropertyInfo { get; }
        public ISqlColumn Column { get; }

        public SqlColumnBinding(SqlColumnBindingAttribute bindingAttribute,
            PropertyInfo propertyInfo,
            ISqlColumn column)
        {
            BindingAttribute = bindingAttribute;
            PropertyInfo = propertyInfo;
            Column = column;
        }
    }
}