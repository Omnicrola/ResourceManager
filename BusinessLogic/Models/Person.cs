using System;

namespace BusinessLogic.Models
{
    public class Person 
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
    }
}
