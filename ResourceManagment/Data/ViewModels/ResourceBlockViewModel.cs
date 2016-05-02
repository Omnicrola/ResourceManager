using System;

namespace ResourceManagment.Data.ViewModels
{
    public class ResourceBlockViewModel :ViewModel
    {
        private ProjectViewModel _project;
        private PersonViewModel _pairPartner;

        public ResourceBlockViewModel(DateTime dateTime)
        {
            Date = dateTime;
        }
        public ProjectViewModel Project { get { return _project; }set { _project = value;  FireOnPropertyChanged("Project"); } }
        public PersonViewModel PairPartner { get { return _pairPartner; } set { _pairPartner = value; FireOnPropertyChanged("PairPartner"); } }
        public DateTime Date { get; private set; }
    }
}