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
        public Action BlockChanged { get; set; }
        public DateTime Date { get; set; }

        public WorkDayViewModel(DateTime day, PersonViewModel person)
        {
            Date = day;
            Morning = new ResourceBlockViewModel(person, day);
            Afternoon = new ResourceBlockViewModel(person, day.Add(TimeSpan.FromHours(12)));
            Morning.PropertyChanged += BlockPropertyUpdated;
        }

        private void BlockPropertyUpdated(object sender, PropertyChangedEventArgs e)
        {
            BlockChanged();
        }


        public void OverwriteBlock(ResourceBlockViewModel resourceBlock)
        {
            var difference = Afternoon.Date.Subtract(resourceBlock.Date).TotalMilliseconds;
            if (difference >= 0)
            {
                Afternoon = resourceBlock;
            }
            else
            {
                Morning = resourceBlock;
            }
        }
    }
}