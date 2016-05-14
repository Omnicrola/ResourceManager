using System;
using System.Configuration;
using System.Data.SQLite;
using DatabaseApi.SqlLite;

namespace ResourceManagment.Data
{
    public class DatabaseMediator : IDisposable
    {
        private readonly SqlSchemaVerifier _schemaVerifier;
        private SQLiteConnection _sqLiteConnection;


        public DatabaseMediator(SqlSchemaVerifier schemaVerifier)
        {
            _schemaVerifier = schemaVerifier;
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
                var databaseLocation = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["sql.database.location"]);
                string connectionString = $"Data Source={databaseLocation}ResourceManagement.sqlite; Version = 3;";

                _sqLiteConnection = new SQLiteConnection(connectionString);
                _sqLiteConnection.Open();
                VerifyStructure();
            }
        }

        private void VerifyStructure()
        {
            _schemaVerifier.Verify(_sqLiteConnection);
        }

        public void Dispose()
        {
            _sqLiteConnection?.Close();
            _sqLiteConnection = null;
        }
    }
}