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
            WeekScheduleViewModel weekSchedule = new WeekScheduleViewModel(DateTime.Now);
            weekSchedule.Schedules.Add(new PersonalScheduleViewModel("ESCH"));
            resourceDataContext.AllSchedules.Add(weekSchedule);
            var mainWindow = new MainWindow(resourceDataContext);
            mainWindow.Show();
        }
    }
}
