using System.Windows;
using ResourceManagment.Operations;

namespace ResourceManagment.Windows.ManagePeople
{
    /// <summary>
    /// Interaction logic for ManagePeopleWindow.xaml
    /// </summary>
    public partial class ManagePeopleWindow : Window
    {
        private readonly AllPeopleViewModel _allPeopleViewModel;
        private readonly UserOperationsBuilder _userOperationsBuilder;

        public ManagePeopleWindow(AllPeopleViewModel peopleViewModel,
            UserOperationsBuilder userOperationsBuilder)
        {
            InitializeComponent();
            DataContext = peopleViewModel;
            _allPeopleViewModel = peopleViewModel;
            _userOperationsBuilder = userOperationsBuilder;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            _allPeopleViewModel.SelectedPerson.FirstName = _allPeopleViewModel.EditedFirstName;
            _allPeopleViewModel.SelectedPerson.LastName = _allPeopleViewModel.EditedLastName;
            _allPeopleViewModel.DataHasChanged = false;
            _userOperationsBuilder.SavePerson(_allPeopleViewModel.SelectedPerson);
        }

        private void ButtonNewProject_Click(object sender, RoutedEventArgs e)
        {
            var newPerson = new PersonViewModel("Bob", "Vila");
            _allPeopleViewModel.People.Add(newPerson);
            _allPeopleViewModel.SelectedPerson = newPerson;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            _allPeopleViewModel.SelectedPerson = null;
            _allPeopleViewModel.EditedFirstName = null;
            _allPeopleViewModel.EditedLastName = null;
        }
    }
}
