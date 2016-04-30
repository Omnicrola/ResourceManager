using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class WeeklySchedule
    {
        public List<PersonalSchedule> Schedule { get; private set; }

        public WeeklySchedule(DateTime weekEnding)
        {
            Schedule = new List<PersonalSchedule>();
        }

    }
}
