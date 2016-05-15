namespace DatabaseApi.SqlLite.Api
{
    public class SqlStringColumn : ISqlColumn
    {
        private readonly int _length;

        public SqlStringColumn(string name, int length) : this(name, length, false) { }
        public SqlStringColumn(string name, int length, bool isNullable)
        {
            Name = name;
            _length = length;
            Nullable = isNullable;
        }

        public string BuildCreateQuery()
        {
            string nullable = Nullable ? "" : "NOT NULL";
            return $"{Name} varchar({_length}) {nullable}";
        }

        public string Name { get; }
        public bool IsPrimaryKey => false;
        public bool Nullable { get; }

        public string EncapsulateValue(object value)
        {
            return $"'{value.ToString()}'";
        }
    }
}