using System.Collections.Generic;
using ResourceManagment.Data.Database.Schema;

namespace ResourceManagment.Data.Database.Tables
{
    public class PersonTable : SqlTable
    {

        public PersonTable()
        {
            TableName = "people";
            Columns = new List<ISqlColumn>
            {
                new SqlIntegerColumn("id", true),
                new SqlStringColumn("firstname", 32),
                new SqlStringColumn("lastname", 32),
            };
        }


    }
}