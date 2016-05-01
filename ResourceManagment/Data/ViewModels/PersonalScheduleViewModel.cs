﻿using System.Collections.ObjectModel;
using System;

namespace ResourceManagment.Data.ViewModels
{
    public class PersonalScheduleViewModel : ViewModel
    {

        private ObservableCollection<WorkDayViewModel> _workDays;
        private DateTime dateTime;
        private PersonViewModel personViewModel;

        public string Initials
        {
            get
            {
                string initials = personViewModel.FirstName.Substring(0, 1) + personViewModel.LastName.Substring(0, 3);
                return initials.ToUpper();
            }
        }
        public ObservableCollection<WorkDayViewModel> Days
        {
            get
            {
                return _workDays;
            }
            set { }
        }

        public WorkDayViewModel Saturday { get { return _workDays[0]; } }
        public WorkDayViewModel Sunday { get { return _workDays[1]; } }
        public WorkDayViewModel Monday { get { return _workDays[2]; } }
        public WorkDayViewModel Tuesday { get { return _workDays[3]; } }
        public WorkDayViewModel Wednesday { get { return _workDays[4]; } }
        public WorkDayViewModel Thursday { get { return _workDays[5]; } }
        public WorkDayViewModel Friday { get { return _workDays[6]; } }

        public PersonalScheduleViewModel(DateTime dateTime, PersonViewModel personViewModel)
        {
            this.dateTime = dateTime;
            this.personViewModel = personViewModel;
            _workDays = new ObservableCollection<WorkDayViewModel>();
            _workDays.Add(new WorkDayViewModel(dateTime.Subtract(TimeSpan.FromDays(6))));
            _workDays.Add(new WorkDayViewModel(dateTime.Subtract(TimeSpan.FromDays(5))));
            _workDays.Add(new WorkDayViewModel(dateTime.Subtract(TimeSpan.FromDays(4))));
            _workDays.Add(new WorkDayViewModel(dateTime.Subtract(TimeSpan.FromDays(3))));
            _workDays.Add(new WorkDayViewModel(dateTime.Subtract(TimeSpan.FromDays(2))));
            _workDays.Add(new WorkDayViewModel(dateTime.Subtract(TimeSpan.FromDays(1))));
            _workDays.Add(new WorkDayViewModel(dateTime.Subtract(TimeSpan.FromDays(0))));
        }
    }
}