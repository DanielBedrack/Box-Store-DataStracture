using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxStore.Model
{
    public class MyQueue<V>
    {
        private QueueNode<V> _head;
        private QueueNode<V> _tail;

        public MyQueue() => Head = Tail = null;
        /// <summary>
        /// Head represent the oldest box
        /// </summary>
        public QueueNode<V> Head { get => _head; private set => _head = value; }
        /// <summary>
        /// Tail represent the newst Box
        /// </summary>
        public QueueNode<V> Tail { get => _tail; private set => _tail = value; }
        public bool IsEmpty() => Head == null && Tail == null;        
        /// <summary>
        /// Adding new Node to Queue
        /// </summary>
        /// <param name="value"></param>
        public void AddToQueue(QueueNode<V> value)
        {
            if (IsEmpty())
            {
                Head = Tail = value;
            }
            else
            {
                Tail.Next = value;
                value.Prev = Tail;
                Tail = value;
            }
        }
        /// <summary>
        /// Removing Node from Queue
        /// </summary>
        /// <param name="toRemove"></param>
        public void Remove(QueueNode<V> toRemove)
        {
            if (IsEmpty() || toRemove == null)
                return;
            if (Head == Tail)
            {
                Head = Tail = null;
            }
            else if (toRemove == Head)
            {                
                toRemove.Next.Prev = null;
                Head = toRemove.Next;
                toRemove.Next = null; 
            }
            else if (toRemove == Tail)
            {
                Tail.Prev.Next = null;
                Tail.Prev = null;                
                Tail = Tail.Prev;
            }
            else
            {
                toRemove.Prev.Next = toRemove.Next;
                toRemove.Next.Prev = toRemove.Prev;
                toRemove.Next = null;
                toRemove.Prev = null;
            }
        }        
    }    
}

