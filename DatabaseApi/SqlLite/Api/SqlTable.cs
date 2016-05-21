using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DatabaseApi.Logging;

namespace DatabaseApi.SqlLite.Api
{
    public abstract class SqlTable<T> : ISqlTable
    {
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
                string columnNames =
                    sqlColumnBindings.Where(b => !b.Column.IsPrimaryKey).Select(b => b.Column.Name).Implode(", ");
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"INSERT INTO {GetTableName()} ");
                stringBuilder.Append($"({columnNames}) ");

                string data = dataObjects.Select(p => CreateSingle(p, sqlColumnBindings)).Implode(", ");
                stringBuilder.Append($" VALUES ({data})");
                stringBuilder.Append(";");

                var query = stringBuilder.ToString();
                DatabaseSchema.ExecuteNonQuery(query);

                dataObjects.ForEach(d => GetPrimaryKeyFor(d, sqlColumnBindings));
            }
        }

        private void GetPrimaryKeyFor(T dataObject, List<SqlColumnBinding> sqlColumnBindings)
        {
            var primaryKeyColumn = sqlColumnBindings.FirstOrDefault(b => b.Column.IsPrimaryKey);


            var stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            stringBuilder.Append(primaryKeyColumn.Column.Name);
            stringBuilder.Append(" FROM ");
            stringBuilder.Append(GetTableName());
            stringBuilder.Append(" WHERE ");

            List<string> columnNameValuePairs = new List<string>();
            foreach (var sqlColumnBinding in sqlColumnBindings)
            {
                if (!sqlColumnBinding.Column.IsPrimaryKey)
                {
                    string columnName = sqlColumnBinding.Column.Name;
                    string value = sqlColumnBinding.EncapsulateValue(dataObject);
                    columnNameValuePairs.Add($"{columnName} = {value}");
                }
            }
            string conditions = columnNameValuePairs.Implode(" AND ");
            stringBuilder.Append(conditions);
            stringBuilder.Append(";");
            string query = stringBuilder.ToString();
            int pkValue = int.Parse(DatabaseSchema.ExecuteScalar(query).ToString());
            primaryKeyColumn.SetValue(dataObject, pkValue);
        }


        public void DataChanged(object valueObject, PropertyChangedEventArgs e)
        {
            try
            {
                var propertyName = e.PropertyName;
                var propertyThatChanged = valueObject.GetType().GetProperty(propertyName);
                var newValue = propertyThatChanged.GetValue(valueObject);
                var sqlColumnBindings = GetColumnBindings(valueObject);
                var sqlColumnBinding = sqlColumnBindings.FirstOrDefault(b => b.PropertyInfo.Equals(propertyThatChanged));
                var primaryKeyBinding = GetPrimaryKeyBinding(sqlColumnBindings);

                if (sqlColumnBinding == null)
                {
                    string errorMessage =
                        $"Attepted to update a property, but table '{GetTableName()}' does not have a column bound " +
                        $"to the property '{propertyName}'";
                    throw new InvalidSqlBindingException(errorMessage);
                }

                var valueString = primaryKeyBinding?.Column.EncapsulateValue(newValue);
                var columnName = sqlColumnBinding?.Column.Name;
                var primaryKey = primaryKeyBinding?.Column.Name;
                var pkValue = primaryKeyBinding?.PropertyInfo.GetValue(valueObject);

                string query =
                    $"UPDATE {GetTableName()} SET {columnName} = {valueString} WHERE {primaryKey} = {pkValue}";
                int result = DatabaseSchema.ExecuteNonQuery(query);
            }
            catch (InvalidSqlBindingException exception)
            {
                DatabaseLogger.Instance.Log(exception);
            }
        }

        private SqlColumnBinding GetPrimaryKeyBinding(List<SqlColumnBinding> sqlColumnBindings)
        {
            var primaryKeyBinding = sqlColumnBindings.FirstOrDefault(b => b.Column.IsPrimaryKey);
            if (primaryKeyBinding == null)
            {
                string errorMessage = $"Attempted to use primary key " +
                                      $"in table '{GetTableName()}', but the table schema has no primary key " +
                                      $"column or one is not defined on the model class.";
                throw new InvalidSqlBindingException(errorMessage);
            }
            return primaryKeyBinding;
        }


        private static string CreateSingle(object dataObject, List<SqlColumnBinding> sqlColumnBindings)
        {
            return sqlColumnBindings
                .Where(b => !b.Column.IsPrimaryKey)
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