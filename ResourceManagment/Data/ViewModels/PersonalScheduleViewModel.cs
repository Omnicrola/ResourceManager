namespace ResourceManagment.Data.ViewModels
{
    public class PersonalScheduleViewModel : ViewModel
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                FireOnPropertyChanged("FirstName");
            }
        }

        PersonalScheduleViewModel(string firstName)
        {
            _firstName = firstName;
        }
    }
}