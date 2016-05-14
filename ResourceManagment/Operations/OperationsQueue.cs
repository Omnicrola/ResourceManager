using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResourceManagment.Operations
{
    public class OperationsQueue
    {
        private readonly object MUTEX = new object();

        private readonly Queue<IDiscreetOperation> _operationsToDo = new Queue<IDiscreetOperation>();
        private readonly Queue<IDiscreetOperation> _newOperations = new Queue<IDiscreetOperation>();

        public OperationsQueue()
        {
            var task = new Task(ProcessQueue);
            task.Start();

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
            LoadNewOperationsIntoQueue();
            ProcessNextTask();
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
    }
}