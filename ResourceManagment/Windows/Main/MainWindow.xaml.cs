using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ResourceManagment.Operations;
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
        private readonly MainWindowViewModel _resourceDataContext;
        private readonly UserOperationsBuilder _userOperationsBuilder;

        public MainWindow(MainWindowViewModel resourceDataContext, UserOperationsBuilder userOperationsBuilder)
        {
            InitializeComponent();
            DataContext = resourceDataContext;
            ResourceDataGrid.People = resourceDataContext.People;
            ResourceDataGrid.Projects = resourceDataContext.Projects;

            _resourceDataContext = resourceDataContext;
            _userOperationsBuilder = userOperationsBuilder;
        }

        private void listOfWeeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _resourceDataContext.SelectedSchedule = (listOfWeeks.SelectedItem as WeekScheduleViewModel);
        }

        private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void MenuItemManagePeople_Click(object sender, RoutedEventArgs e)
        {
            var peopleWindow = new ManagePeopleWindow(new AllPeopleViewModel(_resourceDataContext.People), _userOperationsBuilder);
            peopleWindow.Owner = this;
            peopleWindow.ShowDialog();
        }

        private void MenuItemManageProjects_Click(object sender, RoutedEventArgs e)
        {
            var projectsWindow = new ManageProjects.ManageProjectsWindow(new AllProjectsViewModel(_resourceDataContext.Projects), _userOperationsBuilder);
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
            var selectedSchedule = _resourceDataContext.SelectedSchedule;
            var addResourceWindow =
                new AddResourceWindow(new AddResourceViewModel(_resourceDataContext.People, selectedSchedule), selectedSchedule, _userOperationsBuilder);
            addResourceWindow.Owner = this;
            addResourceWindow.ShowDialog();
        }

        private void buttonAddWeek_Click(object sender, RoutedEventArgs e)
        {
            var weekScheduleViewModel = new WeekScheduleViewModel(DateTime.Now);
            var editableModel = new EditableWeeklyScheduleViewModel(weekScheduleViewModel);
            var editWeeklyScheduleWindow = new EditWeeklyScheduleWindow(editableModel, _userOperationsBuilder) { Owner = this };
            editWeeklyScheduleWindow.ScheduleSaved = () =>
            {
                _resourceDataContext.AllSchedules.Add(weekScheduleViewModel);
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

    }


}
