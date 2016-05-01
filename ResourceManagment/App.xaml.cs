using ResourceManagment.Data.ViewModels;
using System;
using System.Windows;

namespace ResourceManagment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Start(object sender, StartupEventArgs args)
        {
            ResourceDataContext resourceDataContext = new ResourceDataContext();
            WeekScheduleViewModel weekSchedule = new WeekScheduleViewModel(new DateTime(2016, 2, 2));
            resourceDataContext.AllSchedules.Add(weekSchedule);
            var wilmut = CreateProject("Wilmut");
            resourceDataContext.Projects.Add(wilmut);
            resourceDataContext.Projects.Add(CreateProject("Dragonfly"));
            weekSchedule.Schedules.Add(AddSchedule(wilmut));
            var mainWindow = new MainWindow(resourceDataContext);
            mainWindow.Show();
        }

        private ProjectViewModel CreateProject(string name)
        {
            return new ProjectViewModel(name);
        }

        private static PersonalScheduleViewModel AddSchedule(ProjectViewModel project)
        {
            var personalSchedule = new PersonalScheduleViewModel(new DateTime(2016, 4, 4), new PersonViewModel("Eric", "Schreffler"));
            personalSchedule.Monday.Project = project;
            return personalSchedule;
        }
    }
}
