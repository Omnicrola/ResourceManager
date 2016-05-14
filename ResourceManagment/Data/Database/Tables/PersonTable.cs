using System.Collections.Generic;
using DatabaseApi.SqlLite.Api;

namespace ResourceManagment.Data.Database.Tables
{
    public class PersonTable : SqlTable
    {
        public const string TableName = "people";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlStringColumn FirstName = new SqlStringColumn("first_name", 32);
        public static SqlStringColumn LastName = new SqlStringColumn("last_name", 32);

        public PersonTable()
        {
            Columns = new List<ISqlColumn>
            {
                Id,
                FirstName,
                LastName
            };
        }

        public override string GetTableName()
        {
            return TableName;
        }
    }
}