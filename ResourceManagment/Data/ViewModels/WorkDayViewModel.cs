using System;

namespace ResourceManagment.Data.ViewModels
{
    public class WorkDayViewModel
    {
        private DateTime _day;
        private PersonViewModel _pairPartner;
        private ProjectViewModel _project;

        public WorkDayViewModel(DateTime day)
        {
            _day = day;
            _project = null;
            _pairPartner = null;
        }

        public ProjectViewModel Project { get; set; }
        public PersonViewModel PairPartner { get; set; }

        public override string ToString()
        {
            return Project != null ? Project.Name : "None";
        }
    }
}