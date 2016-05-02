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
using System.Windows.Shapes;

namespace ResourceManagment.Windows
{
    /// <summary>
    /// Interaction logic for AlterResourceBlockWindow.xaml
    /// </summary>
    public partial class AlterResourceBlockWindow : Window
    {
        private AlterBlockDataContext _alterBlockDataContext;
        private ResourceBlockViewModel _resourceBlock;

        public AlterResourceBlockWindow(AlterBlockDataContext alterBlockDataContext, ResourceBlockViewModel resourceBlock)
        {
            InitializeComponent();
            DataContext = alterBlockDataContext ;
            _alterBlockDataContext = alterBlockDataContext;
            _resourceBlock = resourceBlock;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _resourceBlock.Project = _alterBlockDataContext.Project;
            _resourceBlock.PairPartner = _alterBlockDataContext.PairPartner;
            Close();
        }
    }
}
