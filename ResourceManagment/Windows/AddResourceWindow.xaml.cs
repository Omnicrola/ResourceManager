﻿using ResourceManagment.Data.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ResourceManagment.Windows
{
    /// <summary>
    /// Interaction logic for AddResourceWindow.xaml
    /// </summary>
    public partial class AddResourceWindow : Window
    {
        private WeekScheduleViewModel _selectedSchedule;
        private AddResourceViewModel _windowViewModel;

        public AddResourceWindow(AddResourceViewModel addResourceViewModel, WeekScheduleViewModel selectedSchedule)
        {
            InitializeComponent();
            DataContext = addResourceViewModel;
            _windowViewModel = addResourceViewModel;
            _selectedSchedule = selectedSchedule;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerson = _windowViewModel.SelectedPerson;
            if (selectedPerson != null && selectedPerson.IsSelectable)
            {
                var personalSchedule = new PersonalScheduleViewModel(_selectedSchedule.WeekEnding, selectedPerson.Person);
                _selectedSchedule.Schedules.Add(personalSchedule);
                Close();
            }
        }
    }
}
