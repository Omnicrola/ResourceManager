using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ResourceManagment.Operations
{
    public class OperationsQueue : IDisposable
    {
        private readonly object MUTEX = new object();

        private readonly Queue<IDiscreetOperation> _operationsToDo = new Queue<IDiscreetOperation>();
        private readonly Queue<IDiscreetOperation> _newOperations = new Queue<IDiscreetOperation>();
        private readonly Thread _thread;
        private bool _isRunning;

        public OperationsQueue()
        {
            _isRunning = true;
            _thread = new Thread(ProcessQueue);
            _thread.Name = "Operations Queue";
            _thread.Priority = ThreadPriority.BelowNormal;
            _thread.Start();

        }

        public void AddOperation(IDiscreetOperation operation)
        {
            lock (MUTEX)
            {
                _newOperations.Enqueue(operation);
            }
        }

        private void ProcessQueue()
        {
            while (_isRunning)
            {
                LoadNewOperationsIntoQueue();
                ProcessNextTask();
                Thread.Sleep(100);
            }
        }

        private void LoadNewOperationsIntoQueue()
        {
            lock (MUTEX)
            {
                while (_newOperations.Count > 0)
                {
                    _operationsToDo.Enqueue(_newOperations.Dequeue());
                }
            }
        }

        private void ProcessNextTask()
        {
            if (_operationsToDo.Count > 0)
            {
                var operation = _operationsToDo.Dequeue();
                operation.DoWork();
            }

        }

        public void Dispose()
        {
            _isRunning = false;
            _thread.Abort();
        }
    }
}