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
        public string Initials
        {
            get
            {
                string initials = _firstName.Substring(0, 1) + _lastName.Substring(0, 3);
                return initials.ToUpper();
            }
            set { }
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} {2}", Initials, FirstName, LastName);
        }

    }
}
