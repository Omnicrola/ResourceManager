using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
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


        public string BuildCreateQuery()
        {
            return
                SqlTables.Select(t => t.BuildCreateQuery())
                    .Aggregate((cumulative, current) => cumulative + "; " + current);
        }

        public int ExecuteNonQuery(string query)
        {
            var sqLiteConnection = GetConnection();
            var sqLiteCommand = new SQLiteCommand(query, sqLiteConnection);
            return sqLiteCommand.ExecuteNonQuery();
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
                string connectionString = $"Data Source={_databaseLocation}ResourceManagement.sqlite; Version = 3;";

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