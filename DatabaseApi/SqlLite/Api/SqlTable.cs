using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DatabaseApi.SqlLite.Api
{
    public abstract class SqlTable<T> : ISqlTable
    {
        public SQLiteConnection Connection { get; set; }
        protected List<ISqlColumn> Columns;
        protected List<SqlForeignKey> ForeignKeys = new List<SqlForeignKey>();
        protected DatabaseSchema DatabaseSchema { get; }

        protected SqlTable(DatabaseSchema databaseSchema)
        {
            DatabaseSchema = databaseSchema;
        }

        public string BuildCreateQuery()
        {
            string columns = Columns.Select(c => c.BuildCreateQuery())
                .Aggregate((total, next) => total + ", " + next);

            string foreignKeys = ForeignKeys.Select(f => f.BuildCreateQuery())
                .Aggregate((total, next) => total + ", " + next);

            string creationQuery = $"CREATE TABLE {GetTableName()}  ({columns}, {foreignKeys});";
            return creationQuery;
        }

        public abstract string GetTableName();

        public void Create(List<T> people)
        {
            if (people.Any())
            {
                var sqlColumnBindings = GetColumnBindings(people[0]);
                string columnNames = sqlColumnBindings.Select(b => b.Column.Name).Implode(", ");
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"INSERT INTO {GetTableName()} ");
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
                            var errorMessage = $"Invalid binding: The table {GetTableName()} could not find a column " +
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
}