using BoxStore.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        BoxManager manager= BoxManager.Instance;
        [TestMethod]
        public void AddBox()=> manager.Add(11, 11, 20);

        [TestMethod]
        public void SearchInDB()=> manager.SearchInDB(2, 2, 20);

        [TestMethod]
        public void ExpireCheck() => manager.ExpireCheck();

        [TestMethod]
        public void PrintInnerTrees() => manager.PrintInnerTrees(5);

        [TestMethod]
        public void GetItem() => manager.GetItem(2,2);

        [TestMethod]
        public void RemoveBox() => manager.RemoveBox(2,2);

        [TestMethod]
        public void GetAllBoxes() => manager.GetAllBoxes(Console.WriteLine);
        [TestMethod]
        public void GetBoxesInTreem() => manager.GetBoxesInTree(2);       
    }
}
