using System;
using System.Data.SQLite;

namespace ResourceManagment.Data
{
    public class DatabaseMediator : IDisposable
    {
        private SQLiteConnection _sqLiteConnection;
        private const string connectionString = "Data Source=ResourceManagement.sqlite;Version=3;";

        public DatabaseMediator()
        {

        }

        private void OpenConnection()
        {
            if (_sqLiteConnection == null)
            {
                _sqLiteConnection = new SQLiteConnection(connectionString);
                _sqLiteConnection.Open();
            }
        }

        public void Dispose()
        {
            _sqLiteConnection?.Close();
            _sqLiteConnection = null;
        }
    }
}