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
            var dataContext = createDataContext();
            var mainWindow = new MainWindow(dataContext);
            mainWindow.Show();
        }

        private ResourceDataContext createDataContext()
        {
            ResourceDataContext resourceDataContext = new ResourceDataContext();
            var wilmut = new ProjectViewModel("Wilmut");
            var dragonfly = new ProjectViewModel("Dragonfly");
            resourceDataContext.Projects.Add(wilmut);
            resourceDataContext.Projects.Add(dragonfly);

            var esch = new PersonViewModel("Eric", "Schreffler");
            var bvil = new PersonViewModel("Bob", "Vila");
            var kbec = new PersonViewModel("Kent", "Beck");

            WeekScheduleViewModel weekSchedule = new WeekScheduleViewModel(new DateTime(2016, 2, 2));
            resourceDataContext.AllSchedules.Add(weekSchedule);

            var eschPersonalSchedule = new PersonalScheduleViewModel(new DateTime(2016, 4, 4), esch);

            eschPersonalSchedule.Monday.Morning.Project = wilmut;
            eschPersonalSchedule.Monday.Morning.PairPartner = bvil;
            eschPersonalSchedule.Monday.Afternoon.Project = wilmut;
            eschPersonalSchedule.Monday.Afternoon.PairPartner = bvil;


            eschPersonalSchedule.Tuesday.Morning.Project = wilmut;
            eschPersonalSchedule.Tuesday.Morning.PairPartner = bvil;
            eschPersonalSchedule.Tuesday.Afternoon.Project = wilmut;
            eschPersonalSchedule.Tuesday.Afternoon.PairPartner = bvil;


            eschPersonalSchedule.Wednesday.Morning.Project = wilmut;
            eschPersonalSchedule.Wednesday.Morning.PairPartner = bvil;
            eschPersonalSchedule.Wednesday.Afternoon.Project = wilmut;
            eschPersonalSchedule.Wednesday.Afternoon.PairPartner = bvil;


            eschPersonalSchedule.Thursday.Morning.Project = wilmut;
            eschPersonalSchedule.Thursday.Morning.PairPartner = bvil;
            eschPersonalSchedule.Thursday.Afternoon.Project = wilmut;
            eschPersonalSchedule.Thursday.Afternoon.PairPartner = bvil;


            eschPersonalSchedule.Friday.Morning.Project = wilmut;
            eschPersonalSchedule.Friday.Morning.PairPartner = bvil;
            eschPersonalSchedule.Friday.Afternoon.Project = wilmut;
            eschPersonalSchedule.Friday.Afternoon.PairPartner = bvil;

            var bvilPersonalSchedule = new PersonalScheduleViewModel(new DateTime(2016, 4, 4), bvil);

            bvilPersonalSchedule.Monday.Morning.Project = wilmut;
            bvilPersonalSchedule.Monday.Morning.PairPartner = esch;
            bvilPersonalSchedule.Monday.Afternoon.Project = wilmut;
            bvilPersonalSchedule.Monday.Afternoon.PairPartner = esch;


            bvilPersonalSchedule.Tuesday.Morning.Project = wilmut;
            bvilPersonalSchedule.Tuesday.Morning.PairPartner = esch;
            bvilPersonalSchedule.Tuesday.Afternoon.Project = wilmut;
            bvilPersonalSchedule.Tuesday.Afternoon.PairPartner = esch;


            bvilPersonalSchedule.Wednesday.Morning.Project = wilmut;
            bvilPersonalSchedule.Wednesday.Morning.PairPartner = esch;
            bvilPersonalSchedule.Wednesday.Afternoon.Project = wilmut;
            bvilPersonalSchedule.Wednesday.Afternoon.PairPartner = esch;

            bvilPersonalSchedule.Thursday.Morning.Project = null;
            bvilPersonalSchedule.Thursday.Morning.PairPartner = null;
            bvilPersonalSchedule.Thursday.Afternoon.Project = null;
            bvilPersonalSchedule.Thursday.Afternoon.PairPartner = null;

            bvilPersonalSchedule.Friday.Morning.Project = wilmut;
            bvilPersonalSchedule.Friday.Morning.PairPartner = esch;
            bvilPersonalSchedule.Friday.Afternoon.Project = wilmut;
            bvilPersonalSchedule.Friday.Afternoon.PairPartner = esch;

            var kbecPersonalSchedule = new PersonalScheduleViewModel(new DateTime(2016, 4, 4), kbec);

            kbecPersonalSchedule.Thursday.Morning.Project = wilmut;
            kbecPersonalSchedule.Thursday.Morning.PairPartner = esch;
            kbecPersonalSchedule.Thursday.Afternoon.Project = wilmut;
            kbecPersonalSchedule.Thursday.Afternoon.PairPartner = esch;

            weekSchedule.Schedules.Add(eschPersonalSchedule);
            weekSchedule.Schedules.Add(bvilPersonalSchedule);
            weekSchedule.Schedules.Add(kbecPersonalSchedule);
            return resourceDataContext;
        }


    }
}
