using System;

namespace BusinessLogic.Models
{
    public class WorkDay
    {
        public DateTime Day { get; set; }
        public Project Project { get; set; }
        public Person PairPartner { get; set; }

        public WorkDay(DateTime date) {
            Day = date;
        }
    }
}
