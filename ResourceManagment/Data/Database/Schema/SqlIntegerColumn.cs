namespace ResourceManagment.Data.Database.Schema
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
    }
}