using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ResourceManagment.Data.ViewModels;

namespace ResourceManagment.Windows.Main
{
    internal class ResourceDragDropHandler
    {
        private const string RESOURCE_BLOCK_DATA = "RESOURCE_BLOCK_DATA";

        private MouseButtonEventArgs _start;
        private Point _startPoint;

        public MouseButtonEventArgs Start
        {
            get { return _start; }
            set
            {
                _start = value;
                _startPoint = value.GetPosition(null);
            }
        }

        public void DragTo(MouseEventArgs mouseEventArgs)
        {
            var diff = _startPoint - mouseEventArgs.GetPosition(null);
            var farEnoughHorizontally = Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance;
            var farEnoughVertically = Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance;
            if (farEnoughVertically || farEnoughHorizontally)
            {
                var source = mouseEventArgs.Source as Button;
                var resourceBlock = source?.DataContext;
                if (resourceBlock != null)
                {
                    var dataObject = new DataObject(RESOURCE_BLOCK_DATA, resourceBlock);
                    DragDrop.DoDragDrop(source, dataObject, DragDropEffects.Copy);
                }
            }
        }

        public void Drop(DragEventArgs dragEventArgs)
        {

            if (dragEventArgs.Data.GetDataPresent(RESOURCE_BLOCK_DATA))
            {
                var resourceBlockToCopy = dragEventArgs.Data.GetData(RESOURCE_BLOCK_DATA) as ResourceBlockViewModel;
                var button = (dragEventArgs.Source as Button);
                var targetResourceBlock = button?.DataContext as ResourceBlockViewModel;
                if (targetResourceBlock != null &&
                    resourceBlockToCopy != null)
                {
                    bool amNotPartneringWithSelf = !targetResourceBlock.Person.Equals(resourceBlockToCopy.PairPartner);
                    if (amNotPartneringWithSelf)
                    {
                        targetResourceBlock.PairPartner = resourceBlockToCopy.PairPartner;
                        targetResourceBlock.Project = resourceBlockToCopy.Project;
                    }
                }
            }
        }

        public void DragOver(object sender, DragEventArgs dragEventArgs)
        {
            if (!dragEventArgs.Data.GetDataPresent(RESOURCE_BLOCK_DATA) || sender == dragEventArgs.Source)
            {
                dragEventArgs.Effects = DragDropEffects.None;
            }

        }
    }
}