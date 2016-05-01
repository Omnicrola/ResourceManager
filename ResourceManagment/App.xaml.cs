using BusinessLogic.Models;
using ResourceManagment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            weekSchedule.Schedules.Add(AddSchedule());
            resourceDataContext.AllSchedules.Add(weekSchedule);
            resourceDataContext.Projects.Add(CreateProject("Wilmut"));
            resourceDataContext.Projects.Add(CreateProject("Dragonfly"));
            var mainWindow = new MainWindow(resourceDataContext);
            mainWindow.Show();
        }

        private ProjectViewModel CreateProject(string name)
        {
            return new ProjectViewModel(new Project { Name = name });
        }

        private static PersonalScheduleViewModel AddSchedule()
        {
            var person = new Person { FirstName = "Eric", LastName = "Schreffler" };
            var personalSchedule = new PersonalSchedule(new DateTime(2016, 4, 4), person);
            return new PersonalScheduleViewModel(personalSchedule);
        }
    }
}
