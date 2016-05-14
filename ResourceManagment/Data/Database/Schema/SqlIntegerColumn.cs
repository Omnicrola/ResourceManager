namespace ResourceManagment.Data.Database.Schema
{
    public class SqlIntegerColumn : ISqlColumn
    {
        private readonly string _name;
        private readonly bool _isPrimaryKey;

        public SqlIntegerColumn(string name, bool isPrimaryKey)
        {
            _name = name;
            _isPrimaryKey = isPrimaryKey;
        }

        public string BuildCreateQuery()
        {
            string pk = _isPrimaryKey ? "PRIMARY KEY" : "";
            return _name + " int " + pk;
        }
    }
}