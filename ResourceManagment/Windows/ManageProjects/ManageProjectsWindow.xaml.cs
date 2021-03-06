﻿using System.Windows;
using System.Windows.Media;
using ResourceManagment.Operations;

namespace ResourceManagment.Windows.ManageProjects
{
    /// <summary>
    /// Interaction logic for ManageProjects.xaml
    /// </summary>
    public partial class ManageProjectsWindow : Window
    {
        private readonly AllProjectsViewModel _allProjectsViewModel;
        private readonly UserOperationsBuilder _operationsBuilder;

        public ManageProjectsWindow(AllProjectsViewModel projectDataContext, UserOperationsBuilder operationsBuilder)
        {
            InitializeComponent();
            DataContext = projectDataContext;
            _allProjectsViewModel = projectDataContext;
            _operationsBuilder = operationsBuilder;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            _allProjectsViewModel.SelectedProject.Name = _allProjectsViewModel.EditedProjectName;
            if (_allProjectsViewModel.EditedColor.HasValue)
            {
                _allProjectsViewModel.SelectedProject.Color = _allProjectsViewModel.EditedColor.Value;
            }

            _operationsBuilder.SaveProject(_allProjectsViewModel.SelectedProject);
        }

        private void ButtonNewProject_Click(object sender, RoutedEventArgs e)
        {
            var newProject = new ProjectViewModel("New Project");
            _allProjectsViewModel.Projects.Add(newProject);
            _allProjectsViewModel.SelectedProject = newProject;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            _allProjectsViewModel.SelectedProject = null;
            _allProjectsViewModel.EditedProjectName = null;
        }
    }
}
