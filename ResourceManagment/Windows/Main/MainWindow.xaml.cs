using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ResourceManagment.Data;
using ResourceManagment.Data.ViewModels;
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
        private ResourceDataContext _resourceDataContext;
        private ResourceDragDropHandler _resourceDragDropHandler;

        public MainWindow(ResourceDataContext resourceDataContext)
        {
            InitializeComponent();
            DataContext = resourceDataContext;
            _resourceDataContext = resourceDataContext;
            _resourceDragDropHandler = new ResourceDragDropHandler();
        }

        private void buttonAddWeek_Click(object sender, RoutedEventArgs e)
        {
            var weekScheduleViewModel = new WeekScheduleViewModel(DateTime.Now);
            var editableModel = new EditableWeeklyScheduleViewModel(weekScheduleViewModel);
            var editWeeklyScheduleWindow = new EditWeeklyScheduleWindow(editableModel) { Owner = this };
            editWeeklyScheduleWindow.ScheduleSaved = () =>
            {
                _resourceDataContext.AllSchedules.Add(weekScheduleViewModel);
            };
            editWeeklyScheduleWindow.ShowDialog();

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
            var peopleWindow = new ManagePeople.ManagePeopleWindow(new AllPeopleViewModel(_resourceDataContext.People));
            peopleWindow.Owner = this;
            peopleWindow.ShowDialog();
        }

        private void MenuItemManageProjects_Click(object sender, RoutedEventArgs e)
        {
            var projectsWindow = new ManageProjects.ManageProjectsWindow(new AllProjectsViewModel(_resourceDataContext.Projects));
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
            var selectedSchedule = _resourceDataContext.SelectedSchedule;
            var addResourceWindow = new AddResourceWindow(new AddResourceViewModel(_resourceDataContext.People, selectedSchedule), selectedSchedule);
            addResourceWindow.Owner = this;
            addResourceWindow.ShowDialog();

        }

        private void Button_AlterResourceBlock(object sender, RoutedEventArgs e)
        {
            ResourceBlockViewModel resourceBlock = (sender as Button).DataContext as ResourceBlockViewModel;
            AlterBlockDataContext alterBlockDataContext = new AlterBlockDataContext(_resourceDataContext.People, _resourceDataContext.Projects, resourceBlock);
            var alterBlockWindow = new AlterResourceBlockWindow(alterBlockDataContext, resourceBlock);
            alterBlockWindow.Owner = this;
            alterBlockWindow.ShowDialog();
        }


        private void ResourceBlock_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _resourceDragDropHandler.StartDragging(e);
        }

        private void ResourceBlock_MouseMove(object sender, MouseEventArgs e)
        {
            var isPressed = e.LeftButton == MouseButtonState.Pressed;
            if (isPressed)
            {
                _resourceDragDropHandler.DragTo(e);
            }
        }

        private void ResourceBlock_Drop(object sender, DragEventArgs e)
        {
            _resourceDragDropHandler.Drop(e);
        }

        private void ResourceBlock_DragEnter(object sender, DragEventArgs e)
        {
            _resourceDragDropHandler.DragOver(sender, e);
        }

        private void ToggleResourceDragMode(object sender, MouseEventArgs e)
        {
            var ctrlIsPressed = (Keyboard.Modifiers & ModifierKeys.Control) != 0;
            var shiftIsPressed = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;
            if (ctrlIsPressed)
            {
                _resourceDragDropHandler.Mode = DragDropMode.SINGLE;
            }
            else if (shiftIsPressed)
            {
                _resourceDragDropHandler.Mode = DragDropMode.PAINT;
            }
            else
            {
                _resourceDragDropHandler.Mode = DragDropMode.NONE;
            }

        }

        private void EditWeeklySchedule_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            var targetSchedule = button.DataContext as WeekScheduleViewModel;
            var editableWeeklySchedule = new EditableWeeklyScheduleViewModel(targetSchedule);
            var editWeeklyScheduleWindow = new EditWeeklyScheduleWindow(editableWeeklySchedule) { Owner = this };
            editWeeklyScheduleWindow.ShowDialog();
        }
    }
}
