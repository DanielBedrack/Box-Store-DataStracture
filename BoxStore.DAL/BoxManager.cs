using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using BoxStore.Model;


namespace BoxStore.DAL
{
    public class BoxManager : IRepository<Box>
    {
        private BST<double, BST<double, Box>> _tree;
        public TreeManager _treeManager;
        private static BoxManager _instance;
        Box box = new Box();
        ConfigData data = new ConfigData();

        public static BoxManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BoxManager();
                }

                return _instance;
            }
        }
        private BoxManager()
        {
            _tree = new BST<double, BST<double, Box>>();
            _treeManager = new TreeManager(_tree);
            DataBase();
            ExpireCheck();
        }

        /// <summary>
        /// adding box/new box
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="amount"></param>
        public void Add(double width, double height, int amount)
        {
            if (width > 0 && height > 0 && amount > 0)
            {
                var key = _treeManager.GetInnerBst(width); //searching key by width
                var box = GetItem(width, height); //getting box
                if (key == null)
                    _treeManager.AddTreeNode(width);
                if (box == null)
                {
                    _treeManager.AddInnerBST(width, height, amount);
                }
                else
                    box.FillBoxes(amount); //filling found box
            }
            else // error input
            {
                Console.WriteLine("Input is not valid");
            }
        }

        /// <summary>
        /// searching in database requested Box or closest
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Box SearchInDB(double width, double height)
        {
            int chose;
            var a = _treeManager.GetInnerBst(width);
            if (a == null)
            {
                Console.WriteLine("Not Found. Would you like a bigger box? press 1 for Yes, any other key for No");
                chose = int.Parse(Console.ReadLine());
                if (Confirm(chose))
                {
                    a = _tree.FindClosest(width, Config.Data.FirstRange, Config.Data.SecondRange, Config.Data.ThirdRange);
                    if (a == null)
                    {
                        return null;
                    }
                    Console.WriteLine("We didnt find the WIDTH you asked. Most closer WIDTH is: " + a._root.ValueNode.Width);
                }
                else
                {
                    return null;
                }
            }
            var b = GetItem(a._root.ValueNode.Width, height);
            if (b == null)
            {
                Console.WriteLine("Not Found. Would you like a bigger box? press 1 for Yes, any other key for No");
                chose = int.Parse(Console.ReadLine());
                if (Confirm(chose))
                {
                    b = a.FindClosest(height, Config.Data.FirstRange, Config.Data.SecondRange, Config.Data.ThirdRange);
                    if (b == null)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return b;
        }
        /// <summary>
        /// reciving request for box from the user
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public IEnumerable SearchInDB(double width, double height, int amount)
        {
            Box box = SearchInDB(width, height); 

            if (box == null)
            {
                Console.WriteLine("NOT FOUND");
                yield break;
            }
            Console.WriteLine($"Box you have requested:\n{box}");
            int amountRequest = box.RequestBox(amount);
            if (amountRequest > 0)
            {
                Console.WriteLine($"Alarm! box width: {box.Width} height: {box.Height} is null. \nRemoving box.");
                RemoveBox(box.Width, box.Height);
            }
            if (amountRequest != 0)
            {
                foreach (Box b in SearchInDB(width, height, amountRequest))
                    yield return b;
            }
        }
        /// <summary>
        /// Adding or Refilling after finding reqested Box
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="amount"></param>       
        public void ExpireCheck()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                if (_treeManager.myQueue.Head != null)
                {
                    if (_treeManager.myQueue.Head.Value.ExpireDate < DateTime.Now)
                    {
                        var a = GetItem(_treeManager.myQueue.Head.Value.Width, _treeManager.myQueue.Head.Value.Height);
                        if (a != null)
                        {
                            Console.WriteLine($"Alarm: {a.ToString()}\nis Expired");
                            RemoveBox(a.Width, a.Height);
                            flag = true;
                        }
                    }
                }
            }
        }
        // Printing
        public void Print(IEnumerable items)
        {
            foreach (var a in items)
            {
                Console.WriteLine(a);
            }
        }
        /// <summary>
        /// Show box by width
        /// </summary>
        /// <param name="x"></param>
        public void PrintInnerTrees(double x)
        {
            Print(GetBoxesInTree(x));
        }
        /// <summary>
        /// getting box
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Box GetItem(double width, double height)
        {
            BST<double, Box> a = _treeManager.GetInnerBst(width);
            if (a == null) return null;
            return a.GetValue(height);
        }
        /// <summary>
        /// Remove Box from tree (and queue)
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void RemoveBox(double width, double height)
        {
            Box box = GetItem(width, height);
            if (box != null)
            {
                var a = _treeManager.GetInnerBst(box.Width);
                a.DeleteNode(box.Height);
                _treeManager.myQueue.Remove(box.BoxQueue);
            }
            else
            {
                Console.WriteLine($"Not Found\n");
            }
        }
        /// <summary>
        /// User Approval for searching a bigger box
        /// </summary>
        /// <param name="chose"></param>
        /// <returns></returns>
        private bool Confirm(int chose)
        {
            if (chose == 1) return true;
            return false;
        }
        /// <summary>
        /// Adding boxes to database
        /// </summary>
        private void DataBase()
        {
            for (int i = 2; i <= 10; i++)
                for (int j = i; j <= 10; j++)
                {
                    Add(i, j, 20);
                }
        }
        /// <summary>
        /// Returns all <see cref="Box"/>'s from specific width <see cref="BinaryTree{K, V}"/>
        /// </summary>
        /// <param name="keyWidth">Inner tree in Main tree</param>
        /// <returns><see cref="Box"/>'s</returns>
        public IEnumerable GetBoxesInTree(double keyWidth)
        {
            BST<double, Box> YTree = _tree.GetValue(keyWidth);

            foreach (Box b in YTree.GetEnumeratorValue()) { yield return b; }
        }
        /// <summary>
        /// Shows all boxes in storage
        /// </summary>
        /// <param name="action"></param>
        public void GetAllBoxes(Action<string> action)
        {
            foreach (var XTree in _tree.InOrderNode())
            {
                var YTree = _tree.GetNode(XTree.KeyNode).ValueNode.InOrderNode();
                foreach (var inner in YTree)
                {
                    action(inner.ValueNode.ToString());
                }
            }
        }
    }
}

