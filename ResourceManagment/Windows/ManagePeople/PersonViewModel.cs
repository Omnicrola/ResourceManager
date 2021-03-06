﻿using System;
using DataApi.Models;
using DatabaseApi.SqlLite.Api;
using ResourceManagment.Data.Filtering.ResourceFilters;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.Main;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManagePeople
{
    public class PersonViewModel : PropertyNotification
    {
        private string _firstName;
        private string _lastName;
        private Role _role;

        public PersonViewModel()
        {
            _firstName = "";
            _lastName = "";
            _role = Role.NONE;
        }

        public PersonViewModel(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            _role = Role.NONE;
        }

        public int? ID { get; set; }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                FireOnPropertyChanged("FirstName");
                FireOnPropertyChanged("Initials");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                FireOnPropertyChanged("LastName");
                FireOnPropertyChanged("Initials");
            }
        }

        public string Initials
        {
            get
            {
                string first = FirstName ?? "";
                string last = LastName ?? "";
                int len1 = Math.Min(first.Length, 1);
                int len2 = Math.Min(last.Length, 3);
                string initials = first.Substring(0, len1) + last.Substring(0, len2);
                return initials.ToUpper();
            }
            set { }
        }

        public Role Role
        {
            get { return this._role; }
            set
            {
                this._role = value;
                FireOnPropertyChanged("Role");
            }
        }

        public override string ToString()
        {
            return $"{Initials} - {FirstName} {LastName}";
        }

    }
}
