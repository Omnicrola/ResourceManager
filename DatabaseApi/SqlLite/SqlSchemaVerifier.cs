using System;
using System.Data.SQLite;

namespace DatabaseApi.SqlLite
{
    public class SqlSchemaVerifier
    {
        private readonly string _expectedSchemaVersion;

        public SqlSchemaVerifier(string expectedVersion)
        {
            _expectedSchemaVersion = expectedVersion;
        }

        public string Verify(SQLiteConnection sqLiteConnection)
        {
            string query = $"SELECT {MetadataTable.Value.Name} FROM {MetadataTable.TableName} WHERE {MetadataTable.Key.Name} LIKE 'schema.version'";
            var sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            try
            {
                return sqLiteCommand.ExecuteScalar()?.ToString();
            }
            catch (SQLiteException)
            {
                return null;
            }
        }
    }

    public class InvalidSchemaVersionException : Exception
    {
        public string ExpectedSchemaVersion { get; }
        public string ActualSchemaVersion { get; }

        public InvalidSchemaVersionException(string expectedSchemaVersion, string actualSchemaVersion)
        {
            ExpectedSchemaVersion = expectedSchemaVersion;
            ActualSchemaVersion = actualSchemaVersion;
        }
    }
}