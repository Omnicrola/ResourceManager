using System.Windows;

namespace ResourceManagment.Windows.ManagePeople
{
    /// <summary>
    /// Interaction logic for ManagePeopleWindow.xaml
    /// </summary>
    public partial class ManagePeopleWindow : Window
    {
        private AllPeopleViewModel _allPeopleViewModel;

        public ManagePeopleWindow(AllPeopleViewModel peopleViewModel)
        {
            InitializeComponent();
            DataContext = peopleViewModel;
            _allPeopleViewModel = peopleViewModel;

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            _allPeopleViewModel.SelectedPerson.FirstName = _allPeopleViewModel.EditedFirstName;
            _allPeopleViewModel.SelectedPerson.LastName = _allPeopleViewModel.EditedLastName;
            _allPeopleViewModel.DataHasChanged = false;
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
