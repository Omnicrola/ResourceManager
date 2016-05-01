﻿using ResourceManagment.Data.ViewModels;
using System.Collections.ObjectModel;

namespace ResourceManagment
{
    public class ResourceDataContext : ViewModel
    {
        private WeekScheduleViewModel _selectedSchedule;

        public ObservableCollection<WeekScheduleViewModel> AllSchedules { get; private set; }
        public WeekScheduleViewModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                _selectedSchedule = value;
                FireOnPropertyChanged("SelectedSchedule");
            }
        }

        public ObservableCollection<ProjectViewModel> Projects { get; internal set; }
        public ObservableCollection<PersonViewModel> People { get; internal set; }

        public ResourceDataContext()
        {
            AllSchedules = new ObservableCollection<WeekScheduleViewModel>();
            Projects = new ObservableCollection<ProjectViewModel>();
            People = new ObservableCollection<PersonViewModel>();
        }
    }
}