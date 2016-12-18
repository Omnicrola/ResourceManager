using System;
using System.Collections.ObjectModel;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManagePeople
{
    public class AllPeopleViewModel : ViewModel
    {
        private PersonViewModel _selectedPerson;

        public ObservableCollection<PersonViewModel> People { get; private set; }

        public AllPeopleViewModel(ObservableCollection<PersonViewModel> people)
        {
            People = people;
        }

        public PersonViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                SetPropertyField(ref _selectedPerson, value);
            }
        }

    }
}