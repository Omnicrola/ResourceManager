using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagment.Data.ViewModels
{
    public class WeekScheduleViewModel : ViewModel
    {
        private DateTime _weekEnding;

        public ObservableCollection<PersonalScheduleViewModel> Schedules { get; private set; }
        public DateTime WeekEnding { get { return _weekEnding; } set { SetPropertyField(ref _weekEnding, value); } }
        public ObservableCollection<RequiredResourceViewModel> RequiredProjectResources { get; set; }

        public Action BlockChanged { get; set; }

        public WeekScheduleViewModel(DateTime weekEnding)
        {
            Schedules = new ObservableCollection<PersonalScheduleViewModel>();
            RequiredProjectResources = new ObservableCollection<RequiredResourceViewModel>();
            WeekEnding = weekEnding;

            Schedules.CollectionChanged += UpdateObservers;
        }

        private void UpdateObservers(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (PersonalScheduleViewModel item in e.NewItems)
            {
                item.ResourceBlockChanged += UpdateRequireResources;
            }
        }

        private void UpdateRequireResources()
        {
            foreach (var requirement in RequiredProjectResources)
            {
                requirement.Recalculate(this);
            }
        }
    }
}
