using System.Collections.Generic;
using DatabaseApi.SqlLite;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Filtering.ResourceFilters;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.Main;

namespace ResourceManagment.Data.Database.Tables
{
    public class PersonTable : SqlTable<IPerson>
    {
        public const string TableName = "people";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true, false);
        public static SqlStringColumn FirstName = new SqlStringColumn("first_name", 32);
        public static SqlStringColumn LastName = new SqlStringColumn("last_name", 32);
        public static SqlEnumColumn Role = new SqlEnumColumn("role", typeof(Role));

        public PersonTable(DatabaseSchema databaseSchema) : base(databaseSchema)
        {
            Columns = new List<ISqlColumn>
            {
                Id,
                FirstName,
                LastName,
                Role
            };
        }

        public override string GetTableName()
        {
            return TableName;
        }

    }
}