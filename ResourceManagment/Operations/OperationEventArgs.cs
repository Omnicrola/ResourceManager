using System;

namespace ResourceManagment.Operations
{
    public class OperationEventArgs : EventArgs
    {
        public IDiscreetOperation CurrentOperation { get; set; }

        public OperationEventArgs(IDiscreetOperation currentOperation)
        {
            CurrentOperation = currentOperation;
        }
    }
}