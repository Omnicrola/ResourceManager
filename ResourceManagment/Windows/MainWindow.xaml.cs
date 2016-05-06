using ResourceManagment.Data.ViewModels;
using ResourceManagment.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResourceManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceDataContext _resourceDataContext;
        private DragHelper _dragHelper;

        public MainWindow(ResourceDataContext resourceDataContext)
        {
            InitializeComponent();
            DataContext = resourceDataContext;
            _resourceDataContext = resourceDataContext;
            _dragHelper = new DragHelper();
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
            var peopleWindow = new ManagePeopleWindow(new AllPeopleViewModel(_resourceDataContext.People));
            peopleWindow.Owner = this;
            peopleWindow.ShowDialog();
        }

        private void MenuItemManageProjects_Click(object sender, RoutedEventArgs e)
        {
            var projectsWindow = new ManageProjectsWindow(new AllProjectsViewModel(_resourceDataContext.Projects));
            projectsWindow.Owner = this;
            projectsWindow.ShowDialog();
        }

        private void MenuItemHelp_Click(object sender, RoutedEventArgs e)
        {
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
            _dragHelper.Start = e;
        }

        private void ResourceBlock_MouseMove(object sender, MouseEventArgs e)
        {
            var isPressed = e.LeftButton == MouseButtonState.Pressed;
            if (isPressed)
            {
                _dragHelper.DragTo(e);
            }
        }

        private void ResourceBlock_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("resourceBlock"))
            {
                var resourceBlockToCopy = e.Data.GetData("resourceBlock") as ResourceBlockViewModel;
                var button = (e.Source as Button);
                var targetResourceBlock = button?.DataContext as ResourceBlockViewModel;
                if (targetResourceBlock != null)
                {
                    if (resourceBlockToCopy != null)
                    {
                        targetResourceBlock.PairPartner = resourceBlockToCopy.PairPartner;
                        targetResourceBlock.Project = resourceBlockToCopy.Project;
                    }
                }
            }
        }

        private void ResourceBlock_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("resourceBlock") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }

    internal class DragHelper
    {
        private MouseButtonEventArgs _start;
        private Point _startPoint;

        public MouseButtonEventArgs Start
        {
            get { return _start; }
            set
            {
                _start = value;
                _startPoint = value.GetPosition(null);
            }
        }

        public void DragTo(MouseEventArgs mouseEventArgs)
        {
            var diff = _startPoint - mouseEventArgs.GetPosition(null);
            var farEnoughHorizontally = Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance;
            var farEnoughVertically = Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance;
            if (farEnoughVertically &&
                farEnoughHorizontally)
            {
                var source = mouseEventArgs.Source as Button;
                var resourceBlock = source?.DataContext;
                if (resourceBlock != null)
                {
                    var dataObject = new DataObject("resourceBlock", resourceBlock);
                    DragDrop.DoDragDrop(source, dataObject, DragDropEffects.Copy);
                }
            }
        }
    }
}
