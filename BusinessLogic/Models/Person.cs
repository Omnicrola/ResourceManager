using System;
using System.ComponentModel;

namespace BusinessLogic.Models
{
    public class Person : INotifyPropertyChanged
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Role Role { get; set; }

        public Person()
        {
            FirstName = "";
            LastName = "";
            Role = Role.None;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
