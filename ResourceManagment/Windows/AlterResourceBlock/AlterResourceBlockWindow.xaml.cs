using System.Windows;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    /// <summary>
    /// Interaction logic for AlterResourceBlockWindow.xaml
    /// </summary>
    public partial class AlterResourceBlockWindow : Window
    {
        private AlterBlockViewModel _alterBlockDataContext;
        private ResourceBlockViewModel _resourceBlock;

        public AlterResourceBlockWindow(AlterBlockViewModel alterBlockDataContext, ResourceBlockViewModel resourceBlock)
        {
            InitializeComponent();
            DataContext = alterBlockDataContext;
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
