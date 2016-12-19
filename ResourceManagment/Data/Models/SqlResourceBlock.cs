using DataApi.Models;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;

namespace ResourceManagment.Data.Models
{
    [SqlTableBinding("weekly_schedules")]
    internal class SqlResourceBlock : IResourceBlock
    {
        [SqlColumnBinding("id")]
        public int? Id { get; set; }

        [SqlColumnBinding("block_order")]
        public int BlockOrder { get; set; }

        [SqlColumnBinding("fk_schedule")]
        public int WeeklyScheduleId { get; set; }

        [SqlColumnBinding("fk_person")]
        public int PersonId { get; set; }

        [SqlColumnBinding("fk_pair_partner")]
        public int? PairPartnerId { get; set; }

        [SqlColumnBinding("fk_project")]
        public int? ProjectId { get; set; }
    }
}