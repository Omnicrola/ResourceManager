namespace ResourceManagment.Data.Database.Schema
{
    public class SqlDateTimeColumn : ISqlColumn
    {

        public SqlDateTimeColumn(string name)
        {
            Name = name;
        }

        public string BuildCreateQuery()
        {
            return Name + " datetime NOT NULL";
        }

        public string Name { get; }
    }
}