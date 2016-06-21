using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;

namespace ResourceManagment.Windows.Main
{
    /// <summary>
    /// Interaction logic for ResourceDataGrid.xaml
    /// </summary>
    public partial class ResourceDataGrid : UserControl
    {
        private readonly ResourceDragDropHandler _resourceDragDropHandler;

        public event Action AddResourceToSchedule;
        public event Action<AlterResourceBlockArgs> ClickAlterResourceBlock;

        public ObservableCollection<PersonViewModel> People { get; set; }
        public ObservableCollection<ProjectViewModel> Projects { get; set; }

        public ResourceDataGrid()
        {
            InitializeComponent();
            _resourceDragDropHandler = new ResourceDragDropHandler();
        }

        private void DisableDataGridSelection(object sender, SelectionChangedEventArgs e)
        {
            ScheduleDataGrid.UnselectAll();
        }

        private void Button_AlterResourceBlock(object sender, RoutedEventArgs e)
        {
            ResourceBlockViewModel resourceBlock = (sender as Button).DataContext as ResourceBlockViewModel;
            ClickAlterResourceBlock?.Invoke(new AlterResourceBlockArgs(resourceBlock));
        }


        private void ResourceBlock_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _resourceDragDropHandler.StartDragging(e);
        }

        private void ResourceBlock_MouseMove(object sender, MouseEventArgs e)
        {
            var isPressed = e.LeftButton == MouseButtonState.Pressed;
            if (isPressed)
            {
                _resourceDragDropHandler.DragTo(e);
            }
        }

        private void ResourceBlock_Drop(object sender, DragEventArgs e)
        {
            _resourceDragDropHandler.Drop(e);
            e.Handled = true;
        }

        private void ResourceBlock_DragEnter(object sender, DragEventArgs e)
        {
            _resourceDragDropHandler.DragOver(sender, e);
            e.Handled = true;
        }

        private void ToggleResourceDragMode(object sender, MouseEventArgs e)
        {
            var ctrlIsPressed = (Keyboard.Modifiers & ModifierKeys.Control) != 0;
            var shiftIsPressed = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;
            if (ctrlIsPressed)
            {
                _resourceDragDropHandler.Mode = DragDropMode.SINGLE;
            }
            else if (shiftIsPressed)
            {
                _resourceDragDropHandler.Mode = DragDropMode.PAINT;
            }
            else
            {
                _resourceDragDropHandler.Mode = DragDropMode.NONE;
            }

        }

        private void AddResource_Click(object sender, RoutedEventArgs e)
        {
            AddResourceToSchedule?.Invoke();
        }
    }

    public class AlterResourceBlockArgs : EventArgs
    {
        public ResourceBlockViewModel ResourceBlock { get; }

        public AlterResourceBlockArgs(ResourceBlockViewModel resourceBlock)
        {
            ResourceBlock = resourceBlock;
        }
    }
}
