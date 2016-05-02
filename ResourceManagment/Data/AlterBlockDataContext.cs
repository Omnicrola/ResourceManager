using ResourceManagment.Data.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace ResourceManagment.Windows
{
    public class AlterBlockDataContext
    {
        private ResourceBlockViewModel _resourceBlock;

        public AlterBlockDataContext(ObservableCollection<PersonViewModel> people, 
            ObservableCollection<ProjectViewModel> projects, 
            ResourceBlockViewModel resourceBlock) {
            People = people;
            Projects = projects;
            _resourceBlock = resourceBlock;
            Project = _resourceBlock.Project;
            PairPartner = _resourceBlock.PairPartner;
        }

        public ObservableCollection<PersonViewModel> People { get; private set; }
        public ObservableCollection<ProjectViewModel> Projects { get; private set; }

        public string ResourceName
        {
            get
            {
                return "N/A";
            }
            set { }
        }

        public ProjectViewModel Project { get; set; }
        public PersonViewModel PairPartner { get; set; }

        public string BlockTime
        {
            get
            {
                var date = _resourceBlock.Date;
                string morningEvening =date.Hour < 12 ? "Morning" : "Afternoon";
                string dayOfWeek = date.DayOfWeek.ToString();
                int dayOfMonth = date.Day;
                int month = date.Month;
                return String.Format("{0} of {1} on {2}/{3}", morningEvening, dayOfWeek, month, dayOfMonth);
            }
            set
            {

            }
        }

    }

}