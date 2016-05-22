using System;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    [SqlTableBinding("resources")]
    public class ResourceBlockViewModel : ViewModel, IResourceBlock
    {
        private ProjectViewModel _project;
        private PersonViewModel _pairPartner;
        private int? _fkPairPartner;
        private int? _fkProject;

        public ResourceBlockViewModel(PersonViewModel person, DateTime dateTime)
        {
            Person = person;
            Date = dateTime;
            Project = ProjectViewModel.Empty;
        }

        [SqlColumnBinding("id")]
        public int? Id { get; set; }

        [SqlColumnBinding("datetime")]
        public DateTime Date { get; private set; }

        [SqlColumnBinding("fk_pair_partner")]
        public int? FkPairPartner
        {
            get { return PairPartner?.ID; }
            set { _fkPairPartner = value; }
        }

        [SqlColumnBinding("fk_project")]
        public int? FkProject
        {
            get { return Project?.Id; }
            set { _fkProject = value; }
        }

        [SqlColumnBinding("fk_schedule")]
        public int? FkSchedule { get; set; }

        public ProjectViewModel Project { get { return _project; } set { _project = value; FireOnPropertyChanged("Project"); } }
        public PersonViewModel PairPartner { get { return _pairPartner; } set { _pairPartner = value; FireOnPropertyChanged("PairPartner"); } }

        public PersonViewModel Person { get; private set; }
    }
}