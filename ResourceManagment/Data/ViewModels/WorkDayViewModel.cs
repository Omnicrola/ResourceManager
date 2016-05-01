using System;

namespace ResourceManagment.Data.ViewModels
{
    public class WorkDayViewModel
    {
        private DateTime _day;

        public ResourceBlockViewModel Morning { get; set; }
        public ResourceBlockViewModel Afternoon { get; set; }

        public WorkDayViewModel(DateTime day)
        {
            _day = day;
            Morning = new ResourceBlockViewModel(day);
            Afternoon = new ResourceBlockViewModel(day.Add(TimeSpan.FromHours(12)));
        }

        public string Day { get { return _day.DayOfWeek.ToString(); } }


    }
}