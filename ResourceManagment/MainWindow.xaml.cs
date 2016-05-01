using BusinessLogic.Models;
using ResourceManagment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResourceManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceDataContext _resourceDataContext;

        public MainWindow(ResourceDataContext resourceDataContext)
        {
            InitializeComponent();
            DataContext = resourceDataContext;
            _resourceDataContext = resourceDataContext;
        }

        private void buttonAddWeek_Click(object sender, RoutedEventArgs e)
        {
            WeekScheduleViewModel weekSchedule = new WeekScheduleViewModel(DateTime.Now);
            weekSchedule.Schedules.Add(new PersonalScheduleViewModel("ESCH"));

            _resourceDataContext.AllSchedules.Add(weekSchedule);
        }

        private void listOfWeeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _resourceDataContext.SelectedSchedule = (listOfWeeks.SelectedItem as WeekScheduleViewModel);
        }


    }
}
