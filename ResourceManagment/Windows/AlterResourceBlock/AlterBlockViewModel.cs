using System;
using System.Collections.ObjectModel;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    public class AlterBlockViewModel
    {
        private ResourceBlockViewModel _resourceBlock;

        public AlterBlockViewModel(ObservableCollection<PersonViewModel> people,
            ObservableCollection<ProjectViewModel> projects,
            ResourceBlockViewModel resourceBlock)
        {
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
                var blockOrder = _resourceBlock.BlockOrder;
                string morningEvening = blockOrder % 2 == 0 ? "Morning" : "Afternoon";
                string dayOfWeek = GetDayOfWeekForBlockOrder(blockOrder);


                return $"{dayOfWeek} {morningEvening}";
            }
            set
            {

            }
        }

        private string GetDayOfWeekForBlockOrder(int blockOrder)
        {
            switch (blockOrder)
            {
                case 1:
                    return "Saturday";
                case 2:
                    return "Saturday";
                case 3:
                    return "Sunday";
                case 4:
                    return "Sunday";
                case 5:
                    return "Monday";
                case 6:
                    return "Monday";
                case 7:
                    return "Tuesday";
                case 8:
                    return "Tuesday";
                case 9:
                    return "Wednesday";
                case 10:
                    return "Wednesday";
                case 11:
                    return "Thursday";
                case 12:
                    return "Thursday";
                case 13:
                    return "Friday";
                case 14:
                    return "Friday";
                default:
                    return "Error";
            }
        }
    }

}