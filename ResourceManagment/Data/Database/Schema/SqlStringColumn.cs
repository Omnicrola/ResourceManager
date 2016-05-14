namespace ResourceManagment.Data.Database.Schema
{
    public class SqlStringColumn : ISqlColumn
    {
        private readonly string _name;
        private readonly int _length;

        public SqlStringColumn(string name, int length)
        {
            _name = name;
            _length = length;
        }

        public string BuildCreateQuery()
        {
            return _name + " varchar(" + _length + ") ";
        }
    }
}