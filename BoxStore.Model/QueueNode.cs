using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxStore.Model
{
    public class QueueNode<V>
    {
        //Fields
        private QueueNode<V> _next = null;
        private QueueNode<V> _prev = null;
        private V _value;
        //Prop
        public V Value { get { return _value; } set { _value = value; } }
        public QueueNode<V> Next { get { return _next; } set { _next = value; } }
        public QueueNode<V> Prev { get { return _prev; } set { _prev = value; } }

        public QueueNode(V val)
        {
            Value = val;
        }
    }
}
