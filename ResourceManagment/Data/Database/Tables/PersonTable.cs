using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DatabaseApi;
using DatabaseApi.SqlLite;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Windows.Main;

namespace ResourceManagment.Data.Database.Tables
{
    public class PersonTable : SqlTable<IPerson>
    {
        public const string TableName = "people";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true);
        public static SqlStringColumn FirstName = new SqlStringColumn("first_name", 32);
        public static SqlStringColumn LastName = new SqlStringColumn("last_name", 32);

        public PersonTable(DatabaseSchema databaseSchema) : base(databaseSchema)
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