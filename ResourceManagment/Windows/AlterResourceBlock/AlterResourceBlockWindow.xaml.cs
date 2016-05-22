using System.Windows;
using ResourceManagment.Operations;

namespace ResourceManagment.Windows.AlterResourceBlock
{
    /// <summary>
    /// Interaction logic for AlterResourceBlockWindow.xaml
    /// </summary>
    public partial class AlterResourceBlockWindow : Window
    {
        private readonly AlterBlockViewModel _alterBlockDataContext;
        private readonly ResourceBlockViewModel _resourceBlock;
        private readonly UserOperationsBuilder _userOperationsBuilder;

        public AlterResourceBlockWindow(AlterBlockViewModel alterBlockDataContext,
            ResourceBlockViewModel resourceBlock,
            UserOperationsBuilder userOperationsBuilder)
        {
            InitializeComponent();
            DataContext = alterBlockDataContext;
            _alterBlockDataContext = alterBlockDataContext;
            _resourceBlock = resourceBlock;
            _userOperationsBuilder = userOperationsBuilder;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _resourceBlock.Project = _alterBlockDataContext.Project;
            _resourceBlock.PairPartner = _alterBlockDataContext.PairPartner;
            _userOperationsBuilder.SaveResourceBlock(_resourceBlock);
            Close();
        }
    }
}
