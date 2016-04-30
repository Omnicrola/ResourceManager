using System;
using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class PersonalSchedule

    {
        public WorkDay[] Days { get; private set; }
        public Person Person { get; set; }

        public PersonalSchedule(DateTime weekEnding, Person person)
        {
            Person = person;
            Days =  new List<WorkDay> {
                new WorkDay(weekEnding.Subtract(TimeSpan.FromDays(6))),
                new WorkDay(weekEnding.Subtract(TimeSpan.FromDays(5))),
                new WorkDay(weekEnding.Subtract(TimeSpan.FromDays(4))),
                new WorkDay(weekEnding.Subtract(TimeSpan.FromDays(3))),
                new WorkDay(weekEnding.Subtract(TimeSpan.FromDays(2))),
                new WorkDay(weekEnding.Subtract(TimeSpan.FromDays(1))),
                new WorkDay(weekEnding.Subtract(TimeSpan.FromDays(0)))
            }.ToArray();
        }
    }
}
