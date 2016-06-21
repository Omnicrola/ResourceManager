using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ResourceManagment.Operations;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.Help;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;

namespace ResourceManagment.Windows.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly UserOperationsBuilder _userOperationsBuilder;

        public MainWindow(MainWindowViewModel mainWindowViewModel, UserOperationsBuilder userOperationsBuilder)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            ResourceDataGrid.People = mainWindowViewModel.People;
            ResourceDataGrid.Projects = mainWindowViewModel.Projects;

            _mainWindowViewModel = mainWindowViewModel;
            _userOperationsBuilder = userOperationsBuilder;
        }

        private void listOfWeeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mainWindowViewModel.SelectedSchedule = (listOfWeeks.SelectedItem as WeekScheduleViewModel);
        }

        private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void MenuItemManagePeople_Click(object sender, EventArgs e)
        {
            var peopleWindow = new ManagePeopleWindow(new AllPeopleViewModel(_mainWindowViewModel.People), _userOperationsBuilder);
            peopleWindow.Owner = this;
            peopleWindow.ShowDialog();
        }

        private void MenuItemManageProjects_Click(object sender, RoutedEventArgs e)
        {
            var projectsWindow = new ManageProjectsWindow(new AllProjectsViewModel(_mainWindowViewModel.Projects), _userOperationsBuilder);
            projectsWindow.Owner = this;
            projectsWindow.ShowDialog();
        }

        private void MenuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow() { Owner = this };
            helpWindow.ShowDialog();
        }

        public void AddResource_Click(object sender, RoutedEventArgs e)
        {
            AddResourceToCurrentSchedule();
        }

        private void ResourceDataGrid_OnAddResourceToSchedule()
        {
            AddResourceToCurrentSchedule();
        }

        private void AddResourceToCurrentSchedule()
        {
            var selectedSchedule = _mainWindowViewModel.SelectedSchedule;
            var addResourceWindow =
                new AddResourceWindow(new AddResourceViewModel(_mainWindowViewModel.People, selectedSchedule), selectedSchedule, _userOperationsBuilder);
            addResourceWindow.Owner = this;
            addResourceWindow.OpenPersonManager += MenuItemManagePeople_Click;
            addResourceWindow.ShowDialog();
        }

        private void buttonAddWeek_Click(object sender, RoutedEventArgs e)
        {
            var weekScheduleViewModel = new WeekScheduleViewModel(DateTime.Now);
            var editableModel = new EditableWeeklyScheduleViewModel(weekScheduleViewModel);
            var editWeeklyScheduleWindow = new EditWeeklyScheduleWindow(editableModel, _userOperationsBuilder) { Owner = this };
            editWeeklyScheduleWindow.ScheduleSaved = () =>
            {
                _mainWindowViewModel.AllSchedules.Add(weekScheduleViewModel);
            };
            editWeeklyScheduleWindow.ShowDialog();

        }

        private void EditWeeklySchedule_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            var targetSchedule = button?.DataContext as WeekScheduleViewModel;
            var editableWeeklySchedule = new EditableWeeklyScheduleViewModel(targetSchedule);
            var editWeeklyScheduleWindow = new EditWeeklyScheduleWindow(editableWeeklySchedule, _userOperationsBuilder) { Owner = this };
            editWeeklyScheduleWindow.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _userOperationsBuilder.Dispose();
        }

        private void ResourceDataGrid_OnClickAlterResourceBlock(AlterResourceBlockArgs eventArgs)
        {
            var resourceBlockViewModel = eventArgs.ResourceBlock;
            AlterBlockViewModel alterBlockDataContext = new AlterBlockViewModel(_mainWindowViewModel.People, _mainWindowViewModel.Projects, resourceBlockViewModel);
            var alterBlockWindow = new AlterResourceBlockWindow(alterBlockDataContext, resourceBlockViewModel, _userOperationsBuilder, _mainWindowViewModel.SelectedSchedule)
            {
                Owner = Window.GetWindow(this)
            };
            alterBlockWindow.ShowDialog();

        }
    }


}
