using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ResourceManagment.Windows.AlterResourceBlock;

namespace ResourceManagment.Windows.Main
{
    internal enum DragDropMode
    {
        NONE, SINGLE, PAINT
    }

    internal class ResourceDragDropHandler
    {
        private const string RESOURCE_BLOCK_DATA = "RESOURCE_BLOCK_DATA";

        private Point _startPoint;

        public DragDropMode Mode { get; set; }

        public ResourceDragDropHandler()
        {
            Mode = DragDropMode.NONE;
        }

        public void StartDragging(MouseButtonEventArgs args)
        {
            _startPoint = args.GetPosition(null);
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
                bool amParteneringWithSelf = IsPartneringWithSelf(dragEventArgs);
                if (!amParteneringWithSelf)
                {
                    var targetResourceBlock = GetResourceTarget(dragEventArgs);
                    var resourceBlockToCopy = GetResourceToCopy(dragEventArgs);
                    targetResourceBlock.PairPartner = resourceBlockToCopy.PairPartner;
                    targetResourceBlock.Project = resourceBlockToCopy.Project;
                }
            }
        }

        public void DragOver(object sender, DragEventArgs dragEventArgs)
        {
            bool dataIsPresent = dragEventArgs.Data.GetDataPresent(RESOURCE_BLOCK_DATA);
            bool cursorHasMovedOffStartingElement = sender != dragEventArgs.Source;

            if (dataIsPresent && cursorHasMovedOffStartingElement)
            {
                if (IsPartneringWithSelf(dragEventArgs))
                {
                    dragEventArgs.Effects = DragDropEffects.None;
                }
            }
            else
            {
                dragEventArgs.Effects = DragDropEffects.None;
            }
        }

        private ResourceBlockViewModel GetResourceToCopy(DragEventArgs dragEventArgs)
        {
            return dragEventArgs.Data.GetData(RESOURCE_BLOCK_DATA) as ResourceBlockViewModel;
        }

        private ResourceBlockViewModel GetResourceTarget(DragEventArgs dragEventArgs)
        {
            return (dragEventArgs.Source as Button)?.DataContext as ResourceBlockViewModel;
        }

        private bool IsPartneringWithSelf(DragEventArgs dragEventArgs)
        {
            var resourceBlockToCopy = dragEventArgs.Data.GetData(RESOURCE_BLOCK_DATA) as ResourceBlockViewModel;
            var button = (dragEventArgs.Source as Button);
            var targetResourceBlock = button?.DataContext as ResourceBlockViewModel;
            if (targetResourceBlock != null &&
                resourceBlockToCopy != null)
            {
                return targetResourceBlock.Person.Equals(resourceBlockToCopy.PairPartner);
            }
            return true;
        }


    }
}