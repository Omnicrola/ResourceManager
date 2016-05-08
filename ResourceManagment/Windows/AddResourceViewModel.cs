﻿using System.Collections.ObjectModel;
using ResourceManagment.Data.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ResourceManagment.Windows
{
    public class AddResourceViewModel : ViewModel
    {
        private WeekScheduleViewModel selectedSchedule;
        private SelectablePersonViewModel _selectedPerson;

        public AddResourceViewModel(ObservableCollection<PersonViewModel> people, WeekScheduleViewModel selectedSchedule)
        {
            this.selectedSchedule = selectedSchedule;
            People = FilterAvailableResources(people);
        }

        private ObservableCollection<SelectablePersonViewModel> FilterAvailableResources(ObservableCollection<PersonViewModel> people)
        {
            List<PersonViewModel> peopleAlreadyScheduled = selectedSchedule.Schedules
                                                                .Select(s => s.Person)
                                                                .ToList();
            var peopleNotScheduled = people.Where(p => !peopleAlreadyScheduled.Contains(p)).ToList();
            var selectableResources = new ObservableCollection<SelectablePersonViewModel>();
            peopleNotScheduled.ForEach(p => selectableResources.Add(new SelectablePersonViewModel(p, true)));
            selectableResources.Add(new SelectablePersonViewModel(new PersonViewModel("----", "----"), false));
            peopleAlreadyScheduled.ForEach(p => selectableResources.Add(new SelectablePersonViewModel(p, false)));
            return selectableResources;
        }

        public ObservableCollection<SelectablePersonViewModel> People { get; private set; }

        public SelectablePersonViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetPropertyField(ref _selectedPerson, value); }
        }
    }
}