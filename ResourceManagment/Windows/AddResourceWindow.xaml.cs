using System.Windows;
using ResourceManagment.Operations;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Windows
{
    /// <summary>
    /// Interaction logic for AddResourceWindow.xaml
    /// </summary>
    public partial class AddResourceWindow : Window
    {
        private readonly WeekScheduleViewModel _selectedSchedule;
        private readonly UserOperationsBuilder _userOperationsBuilder;
        private readonly AddResourceViewModel _windowViewModel;

        public AddResourceWindow(AddResourceViewModel addResourceViewModel, WeekScheduleViewModel selectedSchedule, UserOperationsBuilder userOperationsBuilder)
        {
            InitializeComponent();
            DataContext = addResourceViewModel;
            _windowViewModel = addResourceViewModel;
            _selectedSchedule = selectedSchedule;
            _userOperationsBuilder = userOperationsBuilder;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerson = _windowViewModel.SelectedPerson;
            if (selectedPerson != null && selectedPerson.IsSelectable)
            {
                var personalSchedule = new PersonalScheduleViewModel(_selectedSchedule.WeekEnding, selectedPerson.Person);
                _selectedSchedule.PersonalSchedules.Add(personalSchedule);
                _userOperationsBuilder.SavePersonalSchedule(personalSchedule, _selectedSchedule);
                Close();
            }
        }
    }
}
