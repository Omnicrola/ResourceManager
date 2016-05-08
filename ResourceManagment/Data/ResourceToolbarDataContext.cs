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

        public bool ShowHta { get { return _showHta; } set { SetPropertyField(ref _showHta, value); } }
        public bool ShowDev { get { return _showDev; } set { SetPropertyField(ref _showDev, value); } }
        public bool ShowQa { get { return _showQa; } set { SetPropertyField(ref _showQa, value); } }
        public bool ShowPm { get { return _showPm; } set { SetPropertyField(ref _showPm, value); } }
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