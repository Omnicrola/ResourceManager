using ResourceManagment.Data.ViewModels;

namespace ResourceManagment
{
    public class RoleFilter : IResourceFilter
    {
        private string name;
        private Role targetRole;

        public string FilterName { get { return this.name; } set { } }

        public RoleFilter(string name, Role targetRole)
        {
            this.targetRole = targetRole;
            this.name = name;
        }

        public bool EvalEntirePerson(PersonalScheduleViewModel personalSchedule)
        {
            return personalSchedule.Person.Role == this.targetRole;
        }

        public bool EvalSingleBlock(ResourceBlockViewModel singleBlock)
        {
            return true;
        }
    }
}