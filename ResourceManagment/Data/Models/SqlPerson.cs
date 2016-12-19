using System.ComponentModel;
using DataApi.Models;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Filtering.ResourceFilters;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Data.Models
{
    [SqlTableBinding("people")]
    internal class SqlPerson : IPerson
    {

        [SqlColumnBinding("id")]
        public int? ID { get; set; }

        [SqlColumnBinding("first_name")]
        public string FirstName { get; set; }

        [SqlColumnBinding("last_name")]
        public string LastName { get; set; }

        [SqlColumnBinding("role")]
        public Role Role { get; set; }

    }
}