using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    internal class EditableWeeklyScheduleViewModel : ViewModel, IWeekScheduleViewModel
    {
        private readonly IWeekScheduleViewModel _selectedSchedule;
        private string _notes;
        private DateTime _weekEnding;
        private Color _weekColor;

        public EditableWeeklyScheduleViewModel(IWeekScheduleViewModel selectedSchedule)
        {
            _selectedSchedule = selectedSchedule;
            Notes = selectedSchedule.Notes;
            WeekEnding = selectedSchedule.WeekEnding;
            WeekColor = selectedSchedule.WeekColor;
            Schedules = new ObservableCollection<PersonalScheduleViewModel>();
            RequiredProjectResources = new ObservableCollection<RequiredResourceViewModel>();
            selectedSchedule.Schedules.ToList().ForEach(s => Schedules.Add(s));
            selectedSchedule.RequiredProjectResources.ToList().ForEach(r => RequiredProjectResources.Add(r));
        }

        public ObservableCollection<PersonalScheduleViewModel> Schedules { get; set; }
        public ObservableCollection<RequiredResourceViewModel> RequiredProjectResources { get; set; }

        public string Notes
        {
            get { return _notes; }
            set { SetPropertyField(ref _notes, value); }
        }

        public DateTime WeekEnding
        {
            get { return _weekEnding; }
            set { SetPropertyField(ref _weekEnding, value); }
        }

        public Color WeekColor
        {
            get { return _weekColor; }
            set { SetPropertyField(ref _weekColor, value); }
        }

        public void Save()
        {
            _selectedSchedule.WeekEnding = _weekEnding;
            _selectedSchedule.Notes = _notes;
            _selectedSchedule.WeekColor = _weekColor;
        }
    }
}