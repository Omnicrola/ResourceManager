using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using DatabaseApi.Logging;
using DatabaseApi.SqlLite.Api;

namespace DatabaseApi.SqlLite
{
    public abstract class DatabaseSchema
    {
        protected readonly List<ISqlTable> SqlTables = new List<ISqlTable>();
        private SQLiteConnection _sqLiteConnection;
        private readonly string _databaseLocation;
        private readonly SqlSchemaVerifier _schemaVerifier;

        protected DatabaseSchema(string databaseLocation, SqlSchemaVerifier schemaVerifier)
        {
            _databaseLocation = databaseLocation;
            _schemaVerifier = schemaVerifier;
            SqlTables.Add(new MetadataTable(this));
        }


        public int ExecuteNonQuery(string query)
        {
            DatabaseLogger.Instance.Log(query, LogLevel.INFO);

            var sqLiteConnection = GetConnection();
            var sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            var result = sqLiteCommand.ExecuteNonQuery();

            DatabaseLogger.Instance.Log("Rows affected: " + result, LogLevel.INFO);

            return result;
        }

        public SQLiteConnection GetConnection()
        {
            OpenConnection();
            return _sqLiteConnection;
        }

        private void OpenConnection()
        {
            if (_sqLiteConnection == null)
            {
                Directory.CreateDirectory(_databaseLocation);
                string connectionString = $"Data Source={_databaseLocation}ResourceManagement.sqlite; Version = 3;";

                _sqLiteConnection = new SQLiteConnection(connectionString);
                _sqLiteConnection.Open();
                VerifyStructure();
            }
        }


        private void VerifyStructure()
        {
            bool result = _schemaVerifier.Verify(_sqLiteConnection);
            if (!result)
            {
                CreateSchema();
                _schemaVerifier.SetCurrentVersion(_sqLiteConnection);
            }
        }

        private void CreateSchema()
        {
            SqlTables.ForEach(t =>
            {
                var createQuery = t.BuildCreateQuery();
                ExecuteNonQuery(createQuery);
            });
        }

        public void Dispose()
        {
            _sqLiteConnection?.Close();
            _sqLiteConnection = null;
        }

        public int GetLastInsertRowId()
        {
            OpenConnection();
            string query = "SELECT last_insert_rowid();";
            DatabaseLogger.Instance.Log(query, LogLevel.INFO);
            var sqLiteCommand = new SQLiteCommand(query, _sqLiteConnection);
            int result = int.Parse(sqLiteCommand.ExecuteScalar(CommandBehavior.SingleResult).ToString());

            DatabaseLogger.Instance.Log("Last new rowid: " + result, LogLevel.INFO);
            return result;
        }

        public object ExecuteScalar(string query)
        {
            OpenConnection();
            DatabaseLogger.Instance.Log(query, LogLevel.INFO);
            var sqLiteCommand = new SQLiteCommand(query, _sqLiteConnection);
            object result = sqLiteCommand.ExecuteScalar(CommandBehavior.SingleResult);
            DatabaseLogger.Instance.Log(result?.ToString(), LogLevel.INFO);
            return result;
        }

        public SQLiteDataReader ExecuteQuery(string query)
        {
            OpenConnection();
            DatabaseLogger.Instance.Log(query, LogLevel.INFO);
            var sqLiteCommand = new SQLiteCommand(query, _sqLiteConnection);
            var sqLiteDataReader = sqLiteCommand.ExecuteReader();
            DatabaseLogger.Instance.Log($"Query executed, returned rows : " + sqLiteDataReader.RecordsAffected, LogLevel.INFO);
            return sqLiteDataReader;
        }

    }
}