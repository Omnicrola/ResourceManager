using System;
using System.ComponentModel;
using ResourceManagment.Windows.ManagePeople;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    public class WorkDayViewModel
    {
        private DateTime _day;

        public ResourceBlockViewModel Morning { get; private set; }
        public ResourceBlockViewModel Afternoon { get; private set; }
        public string Day { get { return _day.DayOfWeek.ToString(); } }
        public Action BlockChanged { get; set; }

        public WorkDayViewModel(DateTime day, PersonViewModel person)
        {
            _day = day;
            Morning = new ResourceBlockViewModel(person, day);
            Afternoon = new ResourceBlockViewModel(person, day.Add(TimeSpan.FromHours(12)));
            Morning.PropertyChanged += BlockPropertyUpdated;
        }

        private void BlockPropertyUpdated(object sender, PropertyChangedEventArgs e)
        {
            BlockChanged();
        }


    }
}