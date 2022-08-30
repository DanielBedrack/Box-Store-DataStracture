using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxStore.Model;

namespace BoxStore.DAL
{
    public class TreeManager
    {
        BST<double, BST<double, Box>> Tree;
        public MyQueue<Box> myQueue=new MyQueue<Box>();
        Box box= new Box();
        public TreeManager(BST<double, BST<double, Box>> tree)
        {
            Tree = tree;
        }

        /// <summary>
        /// Adding node to the tree
        /// Node contains BinaryTree
        /// </summary>
        /// <param name="width"> width of the box</param>
        public void AddTreeNode(double width)
        {
            Tree.AddNode(width, new BST<double, Box>());
        }

        /// <summary>
        /// Adding inner node it will contains <see cref="Box"/>
        /// </summary>
        /// <param name="width">width of the box</param>
        /// <param name="height">height of the box</param>
        public void AddInnerBST(double width, double height, int amount)
        {
            var a = Tree.GetValue(width);
            if (amount <= box.MaxAmount)
            {
                Box b = new Box(width, height, amount);
                a.AddNode(height, b);
                myQueue.AddToQueue(b.BoxQueue);
            }
            else
            {
                Box b = new Box(width, height, box.MaxAmount);
                a.AddNode(height, b);
                myQueue.AddToQueue(b.BoxQueue);
            }  
        }

        /// <summary>
        /// Method gets Inner Binary tree by width
        /// </summary>
        /// <param name="width">width of the box</param>
        /// <returns></returns>
        public BST<double, Box> GetInnerBst(double width) // geting inner tree
        {
            return Tree.GetValue(width);
        }
    }
}
