using System;

namespace ResourceManagment
{
    public class PersonViewModel : ViewModel
    {
        private string _firstName;
        private string _lastName;
        private Role _role;

        public PersonViewModel(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            _role = Role.NONE;
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

        public Role Role
        {
            get { return this._role; }
            set
            {
                this.Role = value;
                FireOnPropertyChanged("Role");
            }
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} {2}", Initials, FirstName, LastName);
        }

    }
}
