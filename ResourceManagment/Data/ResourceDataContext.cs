using ResourceManagment.Data.ViewModels;
using System;
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

        public ResourceDataContext()
        {
            AllSchedules = new ObservableCollection<WeekScheduleViewModel>();
            AllSchedules.Add(new WeekScheduleViewModel(DateTime.Now));
        }
    }
}