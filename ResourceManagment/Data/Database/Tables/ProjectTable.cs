using System.Collections.Generic;
using System.ComponentModel;
using DataApi.Models;
using DatabaseApi.SqlLite;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManageProjects;

namespace ResourceManagment.Data.Database.Tables
{
    public class ProjectTable : SqlTable<IProject>
    {
        public const string TableName = "projects";

        public static SqlIntegerColumn Id = new SqlIntegerColumn("id", true, false);
        public static SqlStringColumn Name = new SqlStringColumn("name", 32);
        public static SqlColorColumn Color = new SqlColorColumn("color");

        public ProjectTable(DatabaseSchema databaseSchema) : base(databaseSchema)
        {
            Columns = new List<ISqlColumn>
            {
                Id,
                Name,
                Color
            };
        }


        public override string GetTableName()
        {
            return TableName;
        }

    }
}