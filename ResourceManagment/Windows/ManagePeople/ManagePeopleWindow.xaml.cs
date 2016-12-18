using System.ComponentModel;
using System.Linq;
using System.Windows;
using ResourceManagment.Operations;

namespace ResourceManagment.Windows.ManagePeople
{
    /// <summary>
    /// Interaction logic for ManagePeopleWindow.xaml
    /// </summary>
    public partial class ManagePeopleWindow : Window, INotifyPropertyChanged
    {
        public AllPeopleViewModel PeopleViewModel { get; }
        private readonly UserOperationsBuilder _userOperationsBuilder;
        private PersonViewModel _personBeingEdited;

        public event PropertyChangedEventHandler PropertyChanged;
        public string EditMode
        {
            get { return MatchesExistingPerson(PersonBeingEdited) == null ? "Create" : "Update"; }
            set { }
        }
        public PersonViewModel PersonBeingEdited
        {
            get { return _personBeingEdited; }
            set
            {
                _personBeingEdited = new PersonViewModel(value.FirstName, value.LastName) { ID = value.ID };
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EditMode"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonBeingEdited"));
            }
        }


        public ManagePeopleWindow(AllPeopleViewModel peopleViewModel,
            UserOperationsBuilder userOperationsBuilder)
        {
            InitializeComponent();
            DataContext = this;
            PersonBeingEdited = new PersonViewModel();

            PeopleViewModel = peopleViewModel;
            _userOperationsBuilder = userOperationsBuilder;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var existingPerson = MatchesExistingPerson(PersonBeingEdited);
            if (existingPerson == null)
            {
                PeopleViewModel.People.Add(PersonBeingEdited);
                PeopleViewModel.SelectedPerson = PersonBeingEdited;
            }
            else
            {
                existingPerson.FirstName = PersonBeingEdited.FirstName;
                existingPerson.LastName = PersonBeingEdited.LastName;
                PeopleViewModel.SelectedPerson = existingPerson;
            }
            _userOperationsBuilder.SavePerson(PersonBeingEdited);
            PersonBeingEdited = new PersonViewModel();
        }

        private PersonViewModel MatchesExistingPerson(PersonViewModel person)
        {
            return PeopleViewModel.People.FirstOrDefault(p => p.ID == person.ID);
        }

        private void ButtonNewPerson_Click(object sender, RoutedEventArgs e)
        {
            PersonBeingEdited = new PersonViewModel("New", "Person");
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




    }
}
