using System;
using System.Data.SQLite;
using DatabaseApi.Logging;

namespace DatabaseApi.SqlLite
{
    public class SqlSchemaVerifier
    {
        private const string SCHEMA_VERSION_KEY = "schema.version";

        private readonly string _expectedSchemaVersion;

        public SqlSchemaVerifier(string expectedVersion)
        {
            _expectedSchemaVersion = expectedVersion;
        }

        public bool Verify(SQLiteConnection sqLiteConnection)
        {
            string query = $"SELECT {MetadataTable.Value.Name} FROM {MetadataTable.TableName} WHERE {MetadataTable.Key.Name} LIKE '{SCHEMA_VERSION_KEY}'";
            var sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            try
            {
                var result = sqLiteCommand.ExecuteScalar()?.ToString();
                var versionMatches = _expectedSchemaVersion.Equals(result);
                if (!versionMatches)
                {
                    string message = $"Incompatible database schema. Current expected version is {_expectedSchemaVersion} but found {result}";
                    DatabaseLogger.Instance.Log(message, LogLevel.ERROR);

                }
                return versionMatches;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        public void SetCurrentVersion(SQLiteConnection sqLiteConnection)
        {
            string query = $"UPDATE {MetadataTable.TableName} SET {MetadataTable.Value.Name} = '{_expectedSchemaVersion}' WHERE {MetadataTable.Key.Name} = '{SCHEMA_VERSION_KEY}'";
            var sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();
            DatabaseLogger.Instance.Log($"Set schema version to {_expectedSchemaVersion}", LogLevel.INFO);
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