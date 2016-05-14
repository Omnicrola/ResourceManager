﻿using System;
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
        private readonly OperationsQueue _operationsQueue;

        public MainWindow(MainWindowViewModel resourceDataContext, OperationsQueue operationsQueue)
        {
            InitializeComponent();
            DataContext = resourceDataContext;
            ResourceDataGrid.People = resourceDataContext.People;
            ResourceDataGrid.Projects = resourceDataContext.Projects;

            _resourceDataContext = resourceDataContext;
            _operationsQueue = operationsQueue;

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


        private void EditWeeklySchedule_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            var targetSchedule = button.DataContext as WeekScheduleViewModel;
            var editableWeeklySchedule = new EditableWeeklyScheduleViewModel(targetSchedule);
            var editWeeklyScheduleWindow = new EditWeeklyScheduleWindow(editableWeeklySchedule) { Owner = this };
            editWeeklyScheduleWindow.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _operationsQueue.Dispose();
        }
    }


}
