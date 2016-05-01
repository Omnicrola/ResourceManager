using System;

namespace ResourceManagment
{
    public class PersonViewModel : ViewModel
    {
        private string _firstName;
        private string _lastName;

        public PersonViewModel(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                FireOnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                FireOnPropertyChanged("LastName");
            }
        }

    }
}
