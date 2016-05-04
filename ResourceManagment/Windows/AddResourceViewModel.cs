using System.Collections.ObjectModel;

namespace ResourceManagment.Windows
{
    public class AddResourceViewModel : ViewModel
    {
        private PersonViewModel _selectedPerson;

        public AddResourceViewModel(ObservableCollection<PersonViewModel> people)
        {
            People = people;
        }

        public ObservableCollection<PersonViewModel> People { get; private set; }

        public PersonViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetPropertyField(ref _selectedPerson, value); }
        }
    }
}