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

        public void Verify(SQLiteConnection sqLiteConnection)
        {
            string query = $"SELECT {MetadataTable.Value} FROM {MetadataTable.TableName} WHERE {MetadataTable.Key} LIKE 'schema.version'";
            var sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            var result = sqLiteCommand.ExecuteScalar().ToString();
            if (!result.Equals(_expectedSchemaVersion))
            {
                throw new InvalidSchemaVersionException(_expectedSchemaVersion, result);
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