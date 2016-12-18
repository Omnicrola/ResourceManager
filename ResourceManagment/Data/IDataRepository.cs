using ResourceManagment.Data.Model;

namespace ResourceManagment.Data
{
    public interface IDataRepository
    {
        DataCollection<IPerson> AllPeople { get; }
        DataCollection<IProject> AllProjects { get; }
        DataCollection<IWeeklySchedule> AllWeeklySchedules { get; }
    }
}