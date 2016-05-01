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
            var wilmut = CreateProject("Wilmut");
            var dragonfly = CreateProject("Dragonfly");
            resourceDataContext.Projects.Add(wilmut);
            resourceDataContext.Projects.Add(dragonfly);

            var esch = new PersonViewModel("Eric", "Schreffler");
            var bvil = new PersonViewModel("Bob", "Vila");
            var kbec = new PersonViewModel("Kent", "Beck");

            WeekScheduleViewModel weekSchedule = new WeekScheduleViewModel(new DateTime(2016, 2, 2));
            resourceDataContext.AllSchedules.Add(weekSchedule);

            var eschPersonalSchedule = new PersonalScheduleViewModel(new DateTime(2016, 4, 4), esch);

            eschPersonalSchedule.Monday.Project = wilmut;
            eschPersonalSchedule.Monday.PairPartner = bvil;

            eschPersonalSchedule.Tuesday.Project = wilmut;
            eschPersonalSchedule.Tuesday.PairPartner = bvil;

            eschPersonalSchedule.Wednesday.Project = dragonfly;
            eschPersonalSchedule.Wednesday.PairPartner = bvil;

            eschPersonalSchedule.Thursday.Project = wilmut;
            eschPersonalSchedule.Thursday.PairPartner = kbec;

            eschPersonalSchedule.Friday.Project = wilmut;
            eschPersonalSchedule.Friday.PairPartner = bvil;

            var bvilPersonalSchedule = new PersonalScheduleViewModel(new DateTime(2016, 4, 4), bvil);

            bvilPersonalSchedule.Monday.Project = wilmut;
            bvilPersonalSchedule.Monday.PairPartner = esch;

            bvilPersonalSchedule.Tuesday.Project = wilmut;
            bvilPersonalSchedule.Tuesday.PairPartner = esch;

            bvilPersonalSchedule.Wednesday.Project = dragonfly;
            bvilPersonalSchedule.Wednesday.PairPartner = esch;

            bvilPersonalSchedule.Thursday.Project = null;
            bvilPersonalSchedule.Thursday.PairPartner = null;

            bvilPersonalSchedule.Friday.Project = wilmut;
            bvilPersonalSchedule.Friday.PairPartner = esch;

            var kbecPersonalSchedule = new PersonalScheduleViewModel(new DateTime(2016, 4, 4), kbec);

            kbecPersonalSchedule.Thursday.Project = wilmut;
            kbecPersonalSchedule.Thursday.PairPartner = esch;

            weekSchedule.Schedules.Add(eschPersonalSchedule);
            weekSchedule.Schedules.Add(bvilPersonalSchedule);
            weekSchedule.Schedules.Add(kbecPersonalSchedule);
            return resourceDataContext;
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
