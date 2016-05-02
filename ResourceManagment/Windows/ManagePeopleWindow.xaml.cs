using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResourceManagment.Windows
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
        }

        private void ButtonNewProject_Click(object sender, RoutedEventArgs e)
        {
            var newPerson = new PersonViewModel("Bob", "Vila");
            _allPeopleViewModel.People.Add(newPerson);
            _allPeopleViewModel.SelectedPerson = newPerson;
        }

    }
}
