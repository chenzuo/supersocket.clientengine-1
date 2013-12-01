using System;
﻿using System.Linq;
﻿using System.Net;
using System.Collections.Generic;
﻿using System.Runtime.Serialization;

namespace System.Collections.Concurrent
{
    public class ConcurrentQueue<T> : IProducerConsumerCollection<T>
    {
        private Queue<T> m_Queue;

        private object m_SyncRoot = new object();

        public ConcurrentQueue()
        {
            m_Queue = new Queue<T>();
        }

        public ConcurrentQueue(int capacity)
        {
            m_Queue = new Queue<T>(capacity);
        }

        public ConcurrentQueue(IEnumerable<T> collection)
        {
            m_Queue = new Queue<T>(collection);
        }

        public object SyncRoot
        {
            get { return m_SyncRoot; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public void Enqueue(T item)
        {
            lock (m_SyncRoot)
            {
                m_Queue.Enqueue(item);
            }
        }

        public bool TryAdd(T item)
        {
            Enqueue(item);
            return true;
        }

        public bool TryTake(out T item)
        {
            lock (m_SyncRoot)
            {
                if (m_Queue.Count <= 0)
                {
                    item = default(T);
                    return false;
                }

                item = m_Queue.Dequeue();
                return true;
            }
        }

        public T[] ToArray()
        {
            lock (m_SyncRoot)
            {
                return m_Queue.ToArray();
            }
        }

        public void CopyTo(Array array, int index)
        {
            T[] dest = array as T[];
            if (dest == null)
                return;

            lock (m_SyncRoot)
            {
                m_Queue.CopyTo(dest, index);
            }
        }

        public int Count
        {
            get { return m_Queue.Count; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)InternalGetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return InternalGetEnumerator();
        }

        IEnumerator<T> InternalGetEnumerator()
        {
            return m_Queue.GetEnumerator();
        }
    }
}