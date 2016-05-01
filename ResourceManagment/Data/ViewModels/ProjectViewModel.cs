namespace ResourceManagment
{
    public class ProjectViewModel : ViewModel
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                FireOnPropertyChanged("Name");
            }
        }

        public ProjectViewModel(string name)
        {
            _name = name;
        }
    }
}