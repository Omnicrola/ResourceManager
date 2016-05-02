using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ResourceManagment.Windows
{
    public class AllPeopleViewModel : INotifyPropertyChanged
    {
        private string _editedFirstName;
        private string _editedLastName;
        private PersonViewModel _selectedPerson;

        public ObservableCollection<PersonViewModel> People { get; private set; }

        public AllPeopleViewModel(ObservableCollection<PersonViewModel> people)
        {
            People = people;
        }

        public string Initials
        {
            get
            {
                string first = _editedFirstName != null ? _editedFirstName : "";
                string last = _editedLastName != null ? _editedLastName : "";
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
                _editedFirstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EditedFirstName"));
                PropertyChanged(this, new PropertyChangedEventArgs("Initials"));
            }
        }
        public string EditedLastName
        {
            get { return _editedLastName; }
            set
            {
                _editedLastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EditedLastName"));
                PropertyChanged(this, new PropertyChangedEventArgs("Initials"));
            }
        }
        public PersonViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                EditedFirstName = value.FirstName;
                EditedLastName = value.LastName;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedPerson"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}