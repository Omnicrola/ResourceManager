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
                string connectionString = ConfigurationManager.AppSettings["sql.connection.string"];
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