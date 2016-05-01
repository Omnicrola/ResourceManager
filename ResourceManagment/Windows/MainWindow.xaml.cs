using ResourceManagment.Data.ViewModels;
using ResourceManagment.Windows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ResourceManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceDataContext _resourceDataContext;

        public MainWindow(ResourceDataContext resourceDataContext)
        {
            InitializeComponent();
            DataContext = resourceDataContext;
            _resourceDataContext = resourceDataContext;
        }

        private void buttonAddWeek_Click(object sender, RoutedEventArgs e)
        {
            WeekScheduleViewModel weekSchedule = new WeekScheduleViewModel(new DateTime(2016, 3, 3));
            weekSchedule.Schedules.Add(new PersonalScheduleViewModel(new DateTime(2016, 2, 2), new PersonViewModel("Bob", "Vila")));
            _resourceDataContext.AllSchedules.Add(weekSchedule);
        }

        private void listOfWeeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _resourceDataContext.SelectedSchedule = (listOfWeeks.SelectedItem as WeekScheduleViewModel);
        }

        private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemManagePeople_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemManageProjects_Click(object sender, RoutedEventArgs e)
        {
            var projectsWindow = new ManageProjectsWindow(_resourceDataContext.Projects);
            projectsWindow.Show();
        }

        private void MenuItemHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_AlterResourceBlock(object sender, RoutedEventArgs e)
        {
            ResourceBlockViewModel resourceBlock = (sender as Button).DataContext as ResourceBlockViewModel;
            var alterBlockWindow = new AlterResourceBlockWindow(_resourceDataContext.People, _resourceDataContext.Projects, resourceBlock);
            alterBlockWindow.Show();
        }


    }
}
