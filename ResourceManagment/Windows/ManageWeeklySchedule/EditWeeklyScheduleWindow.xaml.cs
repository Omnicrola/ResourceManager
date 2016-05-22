using System;
using System.Windows;
using ResourceManagment.Operations;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    /// <summary>
    /// Interaction logic for EditWeeklyScheduleWindow.xaml
    /// </summary>
    public partial class EditWeeklyScheduleWindow : Window
    {
        private readonly EditableWeeklyScheduleViewModel _weeklySchedule;
        private readonly UserOperationsBuilder _userOperationsBuilder;

        public EditWeeklyScheduleWindow(EditableWeeklyScheduleViewModel weeklySchedule, UserOperationsBuilder userOperationsBuilder)
        {
            InitializeComponent();
            DataContext = weeklySchedule;
            _weeklySchedule = weeklySchedule;
            _userOperationsBuilder = userOperationsBuilder;
        }

        public Action ScheduleSaved { get; set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _weeklySchedule.Save();
            ScheduleSaved?.Invoke();
            _userOperationsBuilder.SaveWeeklySchedule(_weeklySchedule.ScheduleBeingEdited);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
