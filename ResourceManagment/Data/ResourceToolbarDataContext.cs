namespace ResourceManagment
{
    public class ResourceToolbarDataContext : ViewModel
    {
        private bool _showHta;
        private bool _showDev;
        private bool _showPm;
        private bool _showQa;

        public ResourceToolbarDataContext()
        {
            ShowAll = true;
        }

        public bool ShowHta { get { return _showHta; } set { _showHta = value; FireOnPropertyChanged("ShowHta"); } }
        public bool ShowDev { get { return _showDev; } set { _showDev = value; FireOnPropertyChanged("ShowDev"); } }
        public bool ShowQa { get { return _showQa; } set { _showQa = value; FireOnPropertyChanged("ShowQa"); } }
        public bool ShowPm { get { return _showPm; } set { _showPm = value; FireOnPropertyChanged("ShowPm"); } }
        public bool ShowAll
        {
            get
            { return _showHta && _showDev && _showPm && _showQa; }
            set
            {
                _showHta = _showDev = _showPm = _showQa = value;
                FireOnPropertyChanged("ShowAll");
            }
        }

    }
}