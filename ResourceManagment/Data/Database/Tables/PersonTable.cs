using System.Collections.Generic;
using System.Data.SqlTypes;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public class PersonTable : SqlTable
    {
        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlStringColumn FirstName = new SqlStringColumn("firstname", 32);
        public static SqlStringColumn LastName = new SqlStringColumn("lastname", 32);

        public PersonTable()
        {
            TableName = "people";
            Columns = new List<ISqlColumn>
            {
                Id,
                FirstName,
                LastName
            };
        }

    }
}