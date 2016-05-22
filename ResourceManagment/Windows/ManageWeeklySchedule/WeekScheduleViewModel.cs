using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    [SqlTableBinding("weekly_schedules")]
    public class WeekScheduleViewModel : ViewModel, IWeeklySchedule
    {
        private DateTime _weekEnding;
        private string _notes;
        private Color _weekColor;
        private bool _hasResources;

        public ObservableCollection<PersonalScheduleViewModel> PersonalSchedules { get; set; }
        public ObservableCollection<RequiredResourceViewModel> RequiredProjectResources { get; set; }

        [SqlColumnBinding("id")]
        public int? Id { get; set; }

        [SqlColumnBinding("week_ending")]
        public DateTime WeekEnding { get { return _weekEnding; } set { SetPropertyField(ref _weekEnding, value); } }

        [SqlColumnBinding("color")]
        public Color WeekColor { get { return _weekColor; } set { SetPropertyField(ref _weekColor, value); } }

        public string Notes { get { return _notes; } set { SetPropertyField(ref _notes, value); } }

        public bool HasResources
        {
            get { return _hasResources; }
            set { SetPropertyField(ref _hasResources, value); }
        }

        public Action BlockChanged { get; set; }

        public WeekScheduleViewModel()
        {
            PersonalSchedules = new ObservableCollection<PersonalScheduleViewModel>();
            RequiredProjectResources = new ObservableCollection<RequiredResourceViewModel>();
            WeekColor = Colors.Blue;
            PersonalSchedules.CollectionChanged += UpdateObservers;
        }

        public WeekScheduleViewModel(DateTime weekEnding) : this()
        {
            WeekEnding = weekEnding;
        }

        public void Save()
        {

        }

        private void UpdateObservers(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (PersonalScheduleViewModel item in e.NewItems)
            {
                item.ResourceBlockChanged += UpdateRequireResources;
            }
            HasResources = PersonalSchedules.Count > 0;
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
