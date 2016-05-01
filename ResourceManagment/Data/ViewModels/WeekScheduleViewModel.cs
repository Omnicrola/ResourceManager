using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagment.Data.ViewModels
{
    public class WeekScheduleViewModel : ViewModel
    {
        public ObservableCollection<PersonalScheduleViewModel> Schedules { get; private set; }
        public DateTime WeekEnding { get; private set; }

        public WeekScheduleViewModel(DateTime weekEnding) {
            Schedules = new ObservableCollection<PersonalScheduleViewModel>();
            WeekEnding = weekEnding;
        }
    }
}
