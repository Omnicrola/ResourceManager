using ResourceManagment.Data.ViewModels;
namespace ResourceManagment
{
    public interface IResourceFilter
    {
        bool EvalSingleBlock(ResourceBlockViewModel singleBlock);
        bool EvalEntirePerson(PersonalScheduleViewModel personalSchedule);
        string FilterName { get; set; }
    }
}