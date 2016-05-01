using BusinessLogic.Models;

namespace ResourceManagment.Data.ViewModels
{
    public class WorkDayViewModel
    {
        private WorkDay workDay;

        public WorkDayViewModel(WorkDay workDay)
        {
            this.workDay = workDay;
        }

        public string Project { get { return workDay.Project != null ? workDay.Project.Name : "None"; } }
    }
}