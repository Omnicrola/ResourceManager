using ResourceManagment.Data.Model;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    public static class ResourceBlockExtension
    {
        public static ResourceBlockModel ConvertToSqlModel(this ResourceBlockViewModel resourceBlockViewModel, int scheduleId)
        {
            return new ResourceBlockModel()
            {
                Id = resourceBlockViewModel.Id,
                BlockOrder = resourceBlockViewModel.BlockOrder,
                PairPartnerId = resourceBlockViewModel.PairPartner?.ID,
                PersonId = resourceBlockViewModel.Person.ID.Value,
                ProjectId = resourceBlockViewModel.Project?.Id,
                WeeklyScheduleId = scheduleId
            };
        }

    }
}