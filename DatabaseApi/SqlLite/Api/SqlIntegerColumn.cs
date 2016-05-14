namespace DatabaseApi.SqlLite.Api
{
    public class SqlIntegerColumn : ISqlColumn
    {
        private readonly bool _isPrimaryKey;

        public SqlIntegerColumn(string name, bool isPrimaryKey)
        {
            Name = name;
            _isPrimaryKey = isPrimaryKey;
        }

        public string BuildCreateQuery()
        {
            string pk = _isPrimaryKey ? "PRIMARY KEY" : "";
            return $"{Name} int {pk}";
        }

        public string Name { get; }
        public string EncapsulateValue(object value)
        {
            return $"'{value.ToString()}'";
        }
    }
}