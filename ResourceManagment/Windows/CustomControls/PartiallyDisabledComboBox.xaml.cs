using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ResourceManagment.Windows.CustomControls
{
    /// <summary>
    /// Interaction logic for PartiallyDisabledComboBox.xaml
    /// </summary>
    public partial class PartiallyDisabledComboBox : UserControl
    {

        public PartiallyDisabledComboBox()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public IEnumerable<ISelectableItem> ItemsSource
        {
            get { return (IEnumerable<ISelectableItem>)base.GetValue(ItemsSourceProperty); }
            set
            {
                IEnumerable<ISelectableItem> items = SortItems(value);
                base.SetValue(ItemsSourceProperty, items);
            }
        }

        private static IEnumerable<ISelectableItem> SortItems(IEnumerable<ISelectableItem> unsortedList)
        {
            var sortedItems = new List<ISelectableItem>();
            sortedItems.AddRange(unsortedList.Where(item => item.IsSelectable));
            sortedItems.Add(EmptySelectableItem.INSTANCE);
            sortedItems.AddRange(unsortedList.Where(item => !item.IsSelectable));
            return sortedItems;
        }

        public bool HasValidSelection => SelectedItem?.IsSelectable ?? false;

        public ISelectableItem SelectedItem
        {
            get { return (ISelectableItem)base.GetValue(SelectedItemProperty); }
            set { base.SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable<ISelectableItem>),
            typeof(PartiallyDisabledComboBox));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(ISelectableItem),
            typeof(PartiallyDisabledComboBox));

    }
}
