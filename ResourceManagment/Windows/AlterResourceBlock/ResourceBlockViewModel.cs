using System;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    public class ResourceBlockViewModel : ViewModel
    {
        private ProjectViewModel _project;
        private PersonViewModel _pairPartner;

        public ResourceBlockViewModel(PersonViewModel person, int blockOrder)
        {
            Person = person;
            BlockOrder = blockOrder;
            Project = ProjectViewModel.Empty;
        }

        public int? Id { get; set; }
        public int BlockOrder { get; private set; }
        public PersonViewModel Person { get; private set; }

        public ProjectViewModel Project
        {
            get { return _project; }
            set
            {
                SetPropertyField(ref _project, value);
            }
        }

        public PersonViewModel PairPartner
        {
            get
            {
                return _pairPartner;
            }
            set
            {
                SetPropertyField(ref _pairPartner, value);
            }
        }

        public void Overwrite(ResourceBlockViewModel resourceBlock)
        {
            Id = resourceBlock.Id;
            Person = resourceBlock.Person;
            PairPartner = resourceBlock.PairPartner;
            Project = resourceBlock.Project;
            //            BlockOrder = resourceBlock.BlockOrder;
        }
    }
}