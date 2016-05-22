using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
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
        #region SqlTable Creation
        public string BuildCreateQuery()
        {
            List<string> columnsAndKeys = new List<string>();
            columnsAndKeys.AddRange(Columns.Select(c => c.BuildCreateQuery()));
            columnsAndKeys.AddRange(ForeignKeys.Select(f => f.BuildCreateQuery()));

            string columns = columnsAndKeys.Implode(", ");

            string creationQuery = $"CREATE TABLE IF NOT EXISTS {GetTableName()}  ({columns});";
            return creationQuery;
        }
        #endregion

        public abstract string GetTableName();

        #region SqlTable Data Retrieval
        public List<K> GetAll<K>()
        {
            var sqlColumnBindings = GetColumnBindings(typeof(K));
            string columnNames = sqlColumnBindings.Select(b => b.Column.Name).Implode(", ");
            string query = $"SELECT {columnNames} FROM {GetTableName()}";
            SQLiteDataReader result = DatabaseSchema.ExecuteQuery(query);
            var allTableData = new List<K>();
            while (result.Read())
            {
                K dataObject = Activator.CreateInstance<K>();
                sqlColumnBindings.ForEach(b =>
                {
                    string columnName = b.Column.Name;
                    var value = b.Column.ParseValue(result[columnName]);
                    b.PropertyInfo.SetValue(dataObject, value);

                });
                allTableData.Add(dataObject);
            }
            return allTableData;
        }

        #endregion

        #region SqlTable Data Creation
        public void Create(List<T> dataObjects)
        {
            if (dataObjects.Any())
            {
                var sqlColumnBindings = GetColumnBindings(dataObjects[0].GetType());
                string columnNames =
                    sqlColumnBindings.Where(b => !b.Column.IsPrimaryKey).Select(b => b.Column.Name).Implode(", ");
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"INSERT INTO {GetTableName()} ");
                stringBuilder.Append($"({columnNames}) ");

                string data = dataObjects.Select(p => CreateQueryForSingleInsert(p, sqlColumnBindings)).Implode(", ");
                stringBuilder.Append($" VALUES ({data})");
                stringBuilder.Append(";");

                var query = stringBuilder.ToString();
                DatabaseSchema.ExecuteNonQuery(query);

                dataObjects.ForEach(d => GetPrimaryKeyFor(d, sqlColumnBindings));
            }
        }


        private static string CreateQueryForSingleInsert(object dataObject, List<SqlColumnBinding> sqlColumnBindings)
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

        #endregion

        #region SqlTable Utility Methods
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
                    string eq = value.Equals("NULL") ? "IS" : "=";
                    columnNameValuePairs.Add($"{columnName} {eq} {value}");
                }
            }
            string conditions = columnNameValuePairs.Implode(" AND ");
            stringBuilder.Append(conditions);
            stringBuilder.Append(";");
            string query = stringBuilder.ToString();
            var result = DatabaseSchema.ExecuteScalar(query);
            int pkValue = int.Parse(result.ToString());
            primaryKeyColumn.SetValue(dataObject, pkValue);
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


        private List<SqlColumnBinding> GetColumnBindings(Type objectType)
        {
            var sqlColumnBindings = new List<SqlColumnBinding>();
            var properties = objectType.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
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
                            bool isNullable = propertyInfo.PropertyType.IsGenericType &&
                                propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
                            if (column.IsPrimaryKey && !isNullable)
                            {
                                string errorMessage = $"The Property {propertyInfo.Name} is attempting to bind to column " +
                                                      $"{column.Name} in table {GetTableName()}. This column is" +
                                                      $"designated as the primary key, but the property is not nullable. This is required" +
                                                      $"for detecting insertions and updates.";
                                throw new InvalidSqlBindingException(errorMessage);
                            }
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
        #endregion

        #region SqlTable Data Update

        public void SaveAll(List<T> dataObjects)
        {
            dataObjects.ForEach(Save);
        }

        public void Save(T dataObject)
        {
            var sqlColumnBindings = GetColumnBindings(dataObject.GetType());
            var primaryKeyBinding = GetPrimaryKeyBinding(sqlColumnBindings);
            var primaryKeyValue = primaryKeyBinding.PropertyInfo.GetValue(dataObject);
            var pkColumnName = primaryKeyBinding.Column.Name;

            if (primaryKeyValue == null)
            {
                Create(new List<T> { dataObject });
            }
            else
            {
                var primaryKeyValueString = primaryKeyBinding.Column.EncapsulateValue(primaryKeyValue);
                string query = BuildUpdateQuery(dataObject, sqlColumnBindings, pkColumnName, primaryKeyValueString);
                DatabaseSchema.ExecuteNonQuery(query);
            }

        }

        private string BuildUpdateQuery(T dataObject, List<SqlColumnBinding> sqlColumnBindings, string pkColumnName, string primaryKeyValue)
        {
            string setters = sqlColumnBindings
                .Where(b => !b.Column.IsPrimaryKey)
                .Select(b =>
                {
                    var columnName = b.Column.Name;
                    var value = b.Column.EncapsulateValue(b.PropertyInfo.GetValue(dataObject));
                    return $"{columnName} = {value}";
                })
                .Implode(", ");

            string query = $"UPDATE {GetTableName()} SET {setters} WHERE {pkColumnName} = {primaryKeyValue}";
            return query;
        }

        public void DataChanged(object valueObject, PropertyChangedEventArgs e)
        {
            try
            {
                if (!typeof(T).Equals(valueObject.GetType()))
                {
                    string errorMessage = $"Attempted to update table {GetTableName()} with an object of type {valueObject.GetType()}, " +
                                          $"however this table is bound to objects of type {typeof(T)}";
                    throw new InvalidSqlBindingException(errorMessage);
                }
                var propertyName = e.PropertyName;
                var propertyThatChanged = valueObject.GetType().GetProperty(propertyName);
                var newValue = propertyThatChanged.GetValue(valueObject);
                var sqlColumnBindings = GetColumnBindings(valueObject.GetType());
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
        #endregion
    }
}