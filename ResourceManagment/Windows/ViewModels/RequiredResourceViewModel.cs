using System.Linq;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Windows.ViewModels
{
    public class RequiredResourceViewModel : ViewModel
    {
        private float _percentFilled;
        private ProjectViewModel _project;
        private float _requiredPairs;

        public RequiredResourceViewModel(ProjectViewModel project, float requiredPairs)
        {
            _project = project;
            _requiredPairs = requiredPairs;
        }

        public string Name { get { return _project.Name; } set { } }
        public float PercentFilled { get { return _percentFilled; } set { SetPropertyField(ref _percentFilled, value); } }
        public float RequiredPairs { get { return _requiredPairs; } set { SetPropertyField(ref _requiredPairs, value); } }

        public void Recalculate(WeekScheduleViewModel schedule)
        {
            var blocksMatchingProject = schedule.Schedules
                .SelectMany(s => s.Days)
                .SelectMany(d => new[] { d.Morning, d.Afternoon })
                .ToList()
                .Count;
            PercentFilled = RequiredPairs / blocksMatchingProject / 2f;
        }

    }
}