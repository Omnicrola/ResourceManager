using System;
using System.ComponentModel;
using ResourceManagment.Windows.ManagePeople;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    public class WorkDayViewModel
    {
        public ResourceBlockViewModel Morning { get; private set; }
        public ResourceBlockViewModel Afternoon { get; private set; }
        public string Day { get { return Date.DayOfWeek.ToString(); } }
        public DateTime Date { get; set; }

        public WorkDayViewModel(ResourceBlockViewModel morning, ResourceBlockViewModel afternoon)
        {
            Morning = morning;
            Afternoon = afternoon;
        }
    }
}