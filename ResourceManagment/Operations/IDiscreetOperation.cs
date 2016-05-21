using System;
using System.Windows.Threading;

namespace ResourceManagment.Operations
{

    public interface IDiscreetOperation
    {
        event EventHandler<OperationEventArgs> OperationStarted;
        event EventHandler<OperationEventArgs> OperationFinished;

        void DoWork(Dispatcher mainThreadDispatcher);

        string Description { get; }
    }
}