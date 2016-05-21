using System;
using System.Windows.Threading;

namespace ResourceManagment.Operations
{
    public abstract class AsyncDiscreetOperation : IDiscreetOperation
    {
        public event EventHandler<OperationEventArgs> OperationStarted;
        public event EventHandler<OperationEventArgs> OperationFinished;

        public void DoWork(Dispatcher mainThreadDispatcher)
        {
            mainThreadDispatcher.InvokeAsync(() => OperationStarted?.Invoke(this, new OperationEventArgs()));
            DoWorkInternal();
            mainThreadDispatcher.InvokeAsync(() => OperationFinished?.Invoke(this, new OperationEventArgs()));

        }

        public abstract string Description { get; }

        protected abstract void DoWorkInternal();

    }
}