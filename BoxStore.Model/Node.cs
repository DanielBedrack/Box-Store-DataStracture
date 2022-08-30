using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxStore.Model
{
    public class Node<K,V>
    {
        //Fields
        private K _key;
        private V _value;
        private Node<K, V> _left;
        private Node<K, V> _right;
        //Prop
        public K KeyNode { get { return _key; } }
        public V ValueNode { get { return _value; } set { _value = value; } }
        public Node<K, V> Left { get { return _left; }set { _left = value; } }
        public Node<K, V> Right { get { return _right; } set { _right = value; } }

        public Node(K key, V value)
        {
            _key = key;
            _value = value;
        }
    }
}
