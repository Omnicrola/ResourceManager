using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagment
{
    class PersonViewModel : ViewModel
    {
        private Person _person;

        public PersonViewModel(Person person)
        {
            this._person = person;
        }

        public String FirstName
        {
            get { return _person.FirstName; }
            set {
                _person.FirstName = value;
                FireOnPropertyChanged("FirstName");
            }
        }

    }
}
