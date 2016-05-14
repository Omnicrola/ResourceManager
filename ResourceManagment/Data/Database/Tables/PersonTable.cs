using System.Collections.Generic;
using System.Linq;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Windows.Main;

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

        public void Create(IEnumerable<IPerson> people)
        {
            people.ToList().ForEach(p => CreateSingle(p));
        }

        private void CreateSingle(object dataToSave)
        {
            var properties = dataToSave.GetType().GetProperties();
        }
    }


}