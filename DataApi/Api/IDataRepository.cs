using DataApi.Models;

namespace DataApi.Api
{
    public interface IDataRepository
    {
        DataCollection<IPerson> AllPeople { get; }
        DataCollection<IProject> AllProjects { get; }
        DataCollection<IWeeklySchedule> AllWeeklySchedules { get; }
        DataCollection<IResourceBlock> AllResourceBlocks { get; }
    }
}