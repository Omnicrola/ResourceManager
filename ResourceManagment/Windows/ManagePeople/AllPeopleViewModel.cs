using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ResourceManagment.Windows
{
    public class AllPeopleViewModel : ViewModel
    {
        private string _editedFirstName;
        private string _editedLastName;
        private PersonViewModel _selectedPerson;
        private bool _dataHasChanged;

        public ObservableCollection<PersonViewModel> People { get; private set; }

        public AllPeopleViewModel(ObservableCollection<PersonViewModel> people)
        {
            People = people;
        }

        public string Initials
        {
            get
            {
                string first = _editedFirstName ?? "";
                string last = _editedLastName ?? "";
                int len1 = Math.Min(first.Length, 1);
                int len2 = Math.Min(last.Length, 3);
                string initials = first.Substring(0, len1) + last.Substring(0, len2);
                return initials.ToUpper();
            }
            set { }
        }
        public string EditedFirstName
        {
            get { return _editedFirstName; }
            set
            {
                SetPropertyField(ref _editedFirstName, value);
                NotifyInitialsChanged();
                EvaluateChange();
            }
        }

        public string EditedLastName
        {
            get { return _editedLastName; }
            set
            {
                SetPropertyField(ref _editedLastName, value);
                NotifyInitialsChanged();
                EvaluateChange();
            }
        }

        private void EvaluateChange()
        {
            if (_editedFirstName == null ||
                _editedLastName == null ||
                _selectedPerson == null)
            {
                return;
            }
            bool firstNameChanged = !_editedFirstName.Equals(_selectedPerson.FirstName);
            bool lastNameChanged = !_editedLastName.Equals(_selectedPerson.LastName);
            DataHasChanged = firstNameChanged || lastNameChanged;
        }

        public PersonViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                SetPropertyField(ref _selectedPerson, value);
                if (value != null)
                {
                    EditedFirstName = value.FirstName;
                    EditedLastName = value.LastName;
                }
                DataHasChanged = false;
            }
        }

        private void NotifyInitialsChanged()
        {
            FireOnPropertyChanged("Initials");
        }

        public bool DataHasChanged
        {
            get { return _dataHasChanged; }
            set { SetPropertyField(ref _dataHasChanged, value); }
        }
    }
}