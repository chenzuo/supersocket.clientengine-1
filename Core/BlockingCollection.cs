using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Collections.Concurrent
{
    public class BlockingCollection<T> : Queue<T>
    {
        private readonly object _locker = new object();
        private readonly Queue<T> _itemQ;
        private bool _canAddItems;

        public BlockingCollection()
        {
            _itemQ = new Queue<T>();
            _canAddItems = true;
        }

        public void EnqueueItem(T item)
        {
            lock (_locker)
            {
                _itemQ.Enqueue(item); // We must pulse because we're
                Monitor.Pulse(_locker); // changing a blocking condition.
            }
        }

        public bool TryTake(out T item, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (_canAddItems)
            {
                lock (this)
                {
                    try
                    {
                        item = Dequeue();
                        return true;
                    }
                    catch (Exception)
                    {
                        item = default(T);
                        return false;
                    }
                }
            }

            item = default(T);
            return false;
        }

        public bool TryAdd(T item, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (_canAddItems)
            {
                lock (this)
                {
                    try
                    {
                        Enqueue(item);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public void CompleteAdding()
        {
            _canAddItems = false;
        }
    }

}
