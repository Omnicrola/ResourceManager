using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Data.Filtering.ResourceFilters
{
    public interface IResourceFilter
    {
        bool EvalSingleBlock(ResourceBlockViewModel singleBlock);
        bool EvalEntirePerson(PersonalScheduleViewModel personalSchedule);
        string FilterName { get; set; }
    }
}