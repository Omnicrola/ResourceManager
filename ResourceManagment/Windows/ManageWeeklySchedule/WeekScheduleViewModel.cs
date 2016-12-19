using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media;
using DataApi.Models;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    public class WeekScheduleViewModel : PropertyNotification, IWeeklySchedule
    {
        private DateTime _weekEnding;
        private string _notes;
        private Color _weekColor;
        private bool _hasResources;

        public ObservableCollection<PersonalScheduleViewModel> PersonalSchedules { get; set; }
        public ObservableCollection<RequiredResourceViewModel> RequiredProjectResources { get; set; }

        public int? Id { get; set; }

        public DateTime WeekEnding { get { return _weekEnding; } set { SetPropertyField(ref _weekEnding, value); } }

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
            PersonalSchedules.CollectionChanged += PersonalSchedules_CollectionChanged;
        }

        public WeekScheduleViewModel(DateTime weekEnding) : this()
        {
            WeekEnding = new DateTime(weekEnding.Year, weekEnding.Month, weekEnding.Day);
        }

        public WeekScheduleViewModel(IWeeklySchedule weekEnding)
        {

        }

        public void Save()
        {

        }

        private void PersonalSchedules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HasResources = PersonalSchedules.Count > 0;
        }


        public void OverwriteBlock(ResourceBlockViewModel resourceBlock)
        {
            var personalSchedule = PersonalSchedules.FirstOrDefault(p => p.Person.ID == resourceBlock.Person.ID);
            if (personalSchedule == null)
            {
                personalSchedule = new PersonalScheduleViewModel(resourceBlock.Person);
                PersonalSchedules.Add(personalSchedule);
            }
            personalSchedule.OverwriteBlock(resourceBlock);
        }
    }
}
