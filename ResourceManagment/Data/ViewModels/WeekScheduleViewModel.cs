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
        private DateTime _weekEnding;

        public ObservableCollection<PersonalScheduleViewModel> Schedules { get; private set; }
        public DateTime WeekEnding { get { return _weekEnding; } set { SetPropertyField(ref _weekEnding, value); } }

        public WeekScheduleViewModel(DateTime weekEnding)
        {
            Schedules = new ObservableCollection<PersonalScheduleViewModel>();
            WeekEnding = weekEnding;
        }
    }
}
