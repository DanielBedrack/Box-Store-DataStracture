using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxStore.Model
{
    public class BST<K, V> where K : IComparable<K>
    {
        public Node<K, V> _root;
        static MathProvider<K> _multiplyResult;

        public BST()
        {
            _root = null;
            if (typeof(K) == typeof(double))
                _multiplyResult = new DoubleMathProvide() as MathProvider<K>;
            else if (typeof(K) == null)
            {
                throw new InvalidOperationException($"Type {typeof(K).ToString()} is not suported");
            }
        }
        public void AddNode(K key, V value) 
        {
            if (_root == null)
                _root = new Node<K, V>(key, value);
            else
                AddNode(key, _root, value);
        }
        private void AddNode(K key, Node<K, V> t, V value)
        {
            if (key.CompareTo(t.KeyNode) < 0)
            {
                if (t.Left == null)
                    t.Left = new Node<K, V>(key, value);
                else
                    AddNode(key, t.Left, value);
            }
            else
            {
                if (t.Right == null)
                    t.Right = new Node<K, V>(key, value);
                else
                    AddNode(key, t.Right, value);
            }
        }
        public void DeleteNode(K key)
        {
            var node = GetNode(key);
            var parent = GetParent(key);
            if (node != null)
            {
                if (node != null)
                {
                    // No childs
                    if (node.Left == null && node.Right == null)
                    {
                        if(parent==null)
                        {
                            _root = null;
                            return;
                        }
                        if (parent.Left == node)
                        {
                            parent.Left = null;
                        }
                        else
                        {
                            parent.Right = null;
                        }
                        return;
                    }
                    //Has one Child from left
                    if (node.Left != null && node.Right == null)
                    {
                        if (parent != null)
                        {
                            if (parent.Right == node) 
                            {
                                parent.Right = node.Left; 
                            }
                            else
                            {
                                parent.Left = node.Left;
                            }
                        }
                        else
                        {
                            _root = node.Left;
                        }
                        return;
                    }
                    // Has one child from Right
                    else if (node.Left == null && node.Right != null)
                    {
                        if (parent != null)
                        {
                            if (parent.Right == node) 
                            {
                                parent.Right = node.Right;
                            }
                            else
                            {
                                parent.Left = node.Right;
                            }
                        }
                        else
                        {
                            _root = node.Right;
                        }
                        return;
                    }
                    //Has two children
                    if (node.Left != null && node.Right != null)
                    {
                        var minimum = GetMiniNode(node.Right); //Orgenasion tree
                        var minPerent = GetParent(minimum.KeyNode); //find out his perent
                        if (minimum.Right != null) 
                        {
                            minPerent.Left = minimum.Right; 
                        }
                        minimum.Left = node.Left;                  
                        if (node.Right != minimum)
                        {
                            minimum.Right = node.Right;
                        }
                        else
                        {
                            node.Right = null;
                        }
                        if (minPerent.Left == minimum)
                        {
                            minPerent.Left = null;
                        }
                        if (parent == null)
                        {
                            minimum.Left = node.Left;
                            _root = minimum;
                            AddNode(minimum.Left.KeyNode, minimum.ValueNode);
                        }
                        else if (parent.Right == node)
                            parent.Right = minimum;
                        else
                            parent.Left = minimum;
                    }
                }
            }
        }
        public Node<K, V> GetMiniNode(Node<K, V> t)
        {
            if (t.Left == null)
            {
                return t;
            }
            return GetMiniNode(t.Left);
        }
        public Node<K, V> GetNode(K key)
        {
            if (_root != null)
                return GetNode(key, _root);
            else
                return null;
        }
        private Node<K, V> GetNode(K key, Node<K, V> t)
        {
            if (key.CompareTo(t.KeyNode) == 0)
            {
                return t;
            }
            else if (key.CompareTo(t.KeyNode) < 0)
            {
                return GetNode(key, t.Left);
            }
            else
            {
                return GetNode(key, t.Right);
            }
        }
        public Node<K, V> GetParent(K key)
        {
            if (_root != null)
            {
                return GetParent(key, _root);
            }
            else
            {
                return null;
            }
        }
        private Node<K, V> GetParent(K key, Node<K, V> t)
        {
            if (t == null)
            {
                return null;
            }
            if (t.Left != null && key.CompareTo(t.Left.KeyNode) == 0)
            {
                return t;
            }
            else if (t.Right != null && key.CompareTo(t.Right.KeyNode) == 0)
            {
                return t;
            }
            else if (key.CompareTo(t.KeyNode) < 0)
            {
                return GetParent(key, t.Left);
            }
            else
            {
                return GetParent(key, t.Right);
            }
        }
        public V GetValue(K key)
        {
            if (_root != null)
                return GetValue(key, _root);
            else
                return default(V);
        }
        private V GetValue(K x, Node<K, V> t)
        {
            if (t == null)
            {
                return default(V);
            }
            if (x.CompareTo(t.KeyNode) == 0)
            {
                return t.ValueNode; 
            }
            else if (x.CompareTo(t.KeyNode) < 0)
            {
                return GetValue(x, t.Left);
            }
            else
            {
                return GetValue(x, t.Right);
            }
        }
        public V FindClosest(K key, K firstRange, K secondRange, K thirdRange)
        {
            V a = FindClosest(key, firstRange, _root);
            if (a == null)
            {
                a = FindClosest(key, secondRange, _root);
                if (a == null)
                {
                    a = FindClosest(key, thirdRange, _root);
                }
            }
            return a;
        }
        private V FindClosest(K p, K Xrange, Node<K, V> t)
        {
            K maxRange = _multiplyResult.Multiply(p, Xrange);

            if (t == null)
            {
                return default(V);
            }
            if (t.KeyNode.CompareTo(maxRange) <= 0 && t.KeyNode.CompareTo(p) > 0)
            {
                return t.ValueNode;
            }
            else if (t.KeyNode.CompareTo(maxRange) > 0)
            {
                return FindClosest(p, Xrange, t.Left);
            }
            else
            {
                return FindClosest(p, Xrange, t.Right);
            }
        }
        public IEnumerable GetEnumeratorValue()
        {
                return InOrderValue(_root);
        }
        private IEnumerable InOrderValue(Node<K, V> node)
        {
            if (node != null)
            {
                foreach (V val in InOrderValue(node.Left))
                    yield return val;
                yield return node.ValueNode;
                foreach (V val in InOrderValue(node.Right))
                    yield return val;
            }
        }
        private IEnumerable<Node<K,V>> InOrderNode(Node<K, V> node)
        {
            if (node != null)
            {
                foreach (var val in InOrderNode(node.Left))
                    yield return val;
                yield return node;
                foreach (var val in InOrderNode(node.Right))
                    yield return val;
            }
            else
                yield break;
        }
        public IEnumerable<Node<K, V>> InOrderNode()
        {
            return InOrderNode(_root);
        } 
    }
    public abstract class MathProvider<T>
    {
        public abstract T Multiply(T a, T b);
    }
    public class DoubleMathProvide : MathProvider<double>
    {
        public override double Multiply(double a, double b)
        {
            return a * b;
        }
    }
}

