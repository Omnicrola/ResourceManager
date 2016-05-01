using BusinessLogic.Models;
using System.Collections.ObjectModel;

namespace ResourceManagment.Data.ViewModels
{
    public class PersonalScheduleViewModel : ViewModel
    {

        private PersonalSchedule _personalSchedule;
        private ObservableCollection<WorkDayViewModel> _workDays;

        public string Initials
        {
            get
            {
                string initials = _personalSchedule.Person.FirstName.Substring(0, 1) + _personalSchedule.Person.LastName.Substring(0, 3);
                return initials.ToUpper();
            }
        }
        public ObservableCollection<WorkDayViewModel> Days
        {
            get
            {
                return _workDays;
            }
            set { }
        }

        public string Monday { get { return _workDays[0].Project; } }
        public string Tuesday { get { return _workDays[1].Project; } }
        public string Wednesday { get { return _workDays[2].Project; } }
        public string Thursday { get { return _workDays[3].Project; } }
        public string Friday { get { return _workDays[4].Project; } }

        public PersonalScheduleViewModel(PersonalSchedule personalSchedule)
        {
            _personalSchedule = personalSchedule;
            _workDays = new ObservableCollection<WorkDayViewModel>();
            foreach (WorkDay workDay in _personalSchedule.Days)
            {
                _workDays.Add(new WorkDayViewModel(workDay));
            }
        }
    }
}