using System;
using System.Windows;
using ResourceManagment.Data.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    /// <summary>
    /// Interaction logic for EditWeeklyScheduleWindow.xaml
    /// </summary>
    public partial class EditWeeklyScheduleWindow : Window
    {
        private IWeekScheduleViewModel _weeklySchedule;

        public EditWeeklyScheduleWindow(IWeekScheduleViewModel weeklySchedule)
        {
            InitializeComponent();
            DataContext = weeklySchedule;
            _weeklySchedule = weeklySchedule;
        }

        public Action ScheduleSaved { get; set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _weeklySchedule.Save();
            ScheduleSaved?.Invoke();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
