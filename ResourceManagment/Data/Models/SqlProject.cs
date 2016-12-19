using System.Windows.Media;
using DataApi.Models;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Data.Models
{
    [SqlTableBinding("projects")]
    internal class SqlProject : IProject
    {
        [SqlColumnBinding("id")]
        public int? Id { get; set; }

        [SqlColumnBinding("color")]
        public Color Color { get; set; }

        [SqlColumnBinding("name")]
        public string Name { get; set; }
    }
}