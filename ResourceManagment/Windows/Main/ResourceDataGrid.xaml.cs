using System;
using System.Collections.Generic;
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

namespace ResourceManagment.Windows.Main
{
    /// <summary>
    /// Interaction logic for ResourceDataGrid.xaml
    /// </summary>
    public partial class ResourceDataGrid : UserControl
    {
        public ResourceDataGrid()
        {
            InitializeComponent();
        }

        private void DisableDataGridSelection(object sender, SelectionChangedEventArgs e)
        {
            ScheduleDataGrid.UnselectAll();
        }

        private void Button_AlterResourceBlock(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ResourceBlock_LeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ResourceBlock_MouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ResourceBlock_Drop(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ResourceBlock_DragEnter(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
