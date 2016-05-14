using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public class SqlForeignKey
    {
        private readonly ISqlColumn _localColumn;
        private readonly string _foreignTableName;
        private readonly ISqlColumn _remoteColumn;

        public SqlForeignKey(ISqlColumn localColumn, string foreignTableName, ISqlColumn remoteColumn)
        {
            _localColumn = localColumn;
            _foreignTableName = foreignTableName;
            _remoteColumn = remoteColumn;
        }

        public string BuildCreateQuery()
        {
            return $"FOREIGN KEY({_localColumn.Name}) REFERENCES {_foreignTableName}({_remoteColumn.Name})";
        }
    }
}