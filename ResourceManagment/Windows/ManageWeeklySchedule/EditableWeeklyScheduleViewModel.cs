using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using DataApi.Models;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    public class EditableWeeklyScheduleViewModel : PropertyNotification
    {
        public WeekScheduleViewModel ScheduleBeingEdited { get; }
        private string _notes;
        private DateTime _weekEnding;
        private Color _weekColor;

        public EditableWeeklyScheduleViewModel(WeekScheduleViewModel selectedSchedule)
        {
            ScheduleBeingEdited = selectedSchedule;
            Notes = selectedSchedule.Notes;
            WeekEnding = selectedSchedule.WeekEnding;
            WeekColor = selectedSchedule.WeekColor;
        }


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
            ScheduleBeingEdited.WeekEnding = _weekEnding;
            ScheduleBeingEdited.Notes = _notes;
            ScheduleBeingEdited.WeekColor = _weekColor;
        }
    }
}