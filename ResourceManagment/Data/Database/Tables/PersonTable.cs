using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DatabaseApi;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Windows.Main;

namespace ResourceManagment.Data.Database.Tables
{
    public class PersonTable : SqlTable
    {
        public const string TableName = "people";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlStringColumn FirstName = new SqlStringColumn("first_name", 32);
        public static SqlStringColumn LastName = new SqlStringColumn("last_name", 32);

        public PersonTable()
        {
            Columns = new List<ISqlColumn>
            {
                Id,
                FirstName,
                LastName
            };
        }

        public override string GetTableName()
        {
            return TableName;
        }

        public void Create(List<IPerson> people)
        {
            if (people.Any())
            {
                var sqlColumnBindings = GetColumnBindings(people[0]);
                string columnNames = sqlColumnBindings.Select(b => b.Column.Name).Implode(", ");
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"INSERT INTO {TableName} ");
                stringBuilder.Append($"({columnNames}) ");

                string data = people.Select(p => CreateSingle(p, sqlColumnBindings)).Implode(", ");
                stringBuilder.Append(data);
                stringBuilder.Append(";");

            }
        }

        private static string CreateSingle(object dataObject, List<SqlColumnBinding> sqlColumnBindings)
        {
            return sqlColumnBindings
                .Select(b =>
                {
                    var value = b.PropertyInfo.GetValue(dataObject);
                    return b.Column.EncapsulateValue(value);
                })
                .Implode(", ");
        }

        private List<SqlColumnBinding> GetColumnBindings(object dataToSave)
        {
            var sqlColumnBindings = new List<SqlColumnBinding>();
            var properties = dataToSave.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var customAttributes = propertyInfo.GetCustomAttributes(true);
                foreach (var customAttribute in customAttributes)
                {
                    var bindingAttribute = customAttribute as SqlColumnBindingAttribute;
                    if (bindingAttribute != null)
                    {
                        ISqlColumn column = FindColumn(bindingAttribute);
                        if (column != null)
                        {
                            sqlColumnBindings.Add(new SqlColumnBinding(bindingAttribute, propertyInfo, column));
                        }
                        else
                        {
                            var errorMessage = $"Invalid binding: The table {TableName} could not find a column " +
                                               $"definition for the SqlColumnBindingAttribute of'{bindingAttribute.ColumnName}'";
                            throw new InvalidSqlBindingException(errorMessage);
                        }
                    }
                }
            }
            return sqlColumnBindings;
        }

        private ISqlColumn FindColumn(SqlColumnBindingAttribute bindingAttribute)
        {
            return Columns.First(c => c.Name.Equals(bindingAttribute.ColumnName));
        }
    }

    internal class InvalidSqlBindingException : Exception
    {
        public InvalidSqlBindingException(string errorMessage) : base(errorMessage)
        {
        }
    }

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