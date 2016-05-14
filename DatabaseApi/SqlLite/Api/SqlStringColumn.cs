namespace DatabaseApi.SqlLite.Api
{
    public class SqlStringColumn : ISqlColumn
    {
        private readonly int _length;

        public SqlStringColumn(string name, int length)
        {
            Name = name;
            _length = length;
        }

        public string BuildCreateQuery()
        {
            return $"{Name} varchar({_length})";
        }

        public string Name { get; }
        public string EncapsulateValue(object value)
        {
            return value.ToString();
        }
    }
}