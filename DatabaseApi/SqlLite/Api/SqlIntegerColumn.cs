namespace DatabaseApi.SqlLite.Api
{
    public class SqlIntegerColumn : ISqlColumn
    {

        public SqlIntegerColumn(string name, bool isPrimaryKey)
        {
            Name = name;
            IsPrimaryKey = isPrimaryKey;
        }

        public string BuildCreateQuery()
        {
            string pk = IsPrimaryKey ? "PRIMARY KEY" : "";
            return $"{Name} int {pk}";
        }

        public string Name { get; }
        public bool IsPrimaryKey { get; }

        public string EncapsulateValue(object value)
        {
            return $"'{value.ToString()}'";
        }
    }
}