using System.Data;
using System.Data.SQLite;

namespace ResourceManagment.Data
{
    public class DatabaseSchema
    {
        public static void PersonTable(SQLiteConnection connection)
        {
            string query = "CREATE TABLE " + Person.TABLE_NAME + "(" +
                           "id int PRIMARY KEY," +
                           "firstname varchar(32) NOT NULL" +
                           "lastname varchar(32) NOT NULL" +
                           "role int NOT NULL" +
                                                               ")";
            var sqLiteCommand = new SQLiteCommand(query, connection);
            sqLiteCommand.ExecuteNonQuery();
        }
    }

    public static class Person
    {
        public const string TABLE_NAME = "people";
    }
}