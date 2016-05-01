using System;

namespace ResourceManagment.Data.ViewModels
{
    public class ResourceBlockViewModel
    {
        public ResourceBlockViewModel(DateTime dateTime)
        {
            Date = dateTime;
        }
        public ProjectViewModel Project { get; set; }
        public PersonViewModel PairPartner { get; set; }
        public DateTime Date { get; private set; }
    }
}