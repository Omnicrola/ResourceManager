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
            string pk = IsPrimaryKey ? "PRIMARY KEY AUTOINCREMENT" : "";
            string nullable = Nullable ? "" : "NOT NULL";
            return $"{Name} INTEGER {pk} {nullable}";
        }

        public string Name { get; }
        public bool IsPrimaryKey { get; }
        public bool Nullable => false;

        public string EncapsulateValue(object value)
        {
            return $"'{value.ToString()}'";
        }
    }
}