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
            List<string> columnsAndKeys = new List<string>();
            columnsAndKeys.AddRange(Columns.Select(c => c.BuildCreateQuery()));
            columnsAndKeys.AddRange(ForeignKeys.Select(f => f.BuildCreateQuery()));

            string columns = columnsAndKeys.Implode(", ");

            string creationQuery = $"CREATE TABLE IF NOT EXISTS {GetTableName()}  ({columns});";
            return creationQuery;
        }

        public abstract string GetTableName();

        public void Create(List<T> dataObjects)
        {
            if (dataObjects.Any())
            {
                var sqlColumnBindings = GetColumnBindings(dataObjects[0]);
                string columnNames = sqlColumnBindings.Select(b => b.Column.Name).Implode(", ");
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"INSERT INTO {GetTableName()} ");
                stringBuilder.Append($"({columnNames}) ");

                string data = dataObjects.Select(p => CreateSingle(p, sqlColumnBindings)).Implode(", ");
                stringBuilder.Append($" VALUES ({data})");
                stringBuilder.Append(";");

                var query = stringBuilder.ToString();
                DatabaseSchema.ExecuteNonQuery(query);
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
            return Columns.FirstOrDefault(c => c.Name.Equals(bindingAttribute.ColumnName));
        }
    }
}