﻿using System;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    public class ResourceBlockViewModel : ViewModel
    {
        private ProjectViewModel _project;
        private PersonViewModel _pairPartner;

        public ResourceBlockViewModel(PersonViewModel person, DateTime dateTime)
        {
            Person = person;
            Date = dateTime;
            Project = ProjectViewModel.Empty;
        }
        public ProjectViewModel Project { get { return _project; } set { _project = value; FireOnPropertyChanged("Project"); } }
        public PersonViewModel PairPartner { get { return _pairPartner; } set { _pairPartner = value; FireOnPropertyChanged("PairPartner"); } }
        public DateTime Date { get; private set; }
        public PersonViewModel Person { get; private set; }
    }
}