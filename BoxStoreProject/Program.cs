using BoxStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxStore.DAL;


namespace BoxStoreProject
{
    internal class Program
    {
        public static void Display(string s)
        {
            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            BoxManager manager = BoxManager.Instance;
            
            bool StorageSystem = true;
            while (StorageSystem)
            {
                Display("Welcome to the computer box storage managment");
                manager.ExpireCheck();
                Display($"Press 1 to get Boxes \nPress 2 to Add \nPress 3 to Remove \nPress 4 to show" +
                    $" available Boxes \nPress 5 to Show all Boxes by Width \nPress 6 to Exit ");
                int chose = int.Parse(Console.ReadLine());
                switch (chose)
                {
                    case 1:
                        Display("Enter width, height, amount");
                        var search= manager.SearchInDB(double.Parse(Console.ReadLine()), double.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
                        foreach(var item in search)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 2:
                        Display("Enter width, height, amount");
                        manager.Add(double.Parse(Console.ReadLine()), double.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
                        break;
                    case 3:
                        Display("Enter width and height");
                        manager.RemoveBox(double.Parse(Console.ReadLine()), double.Parse(Console.ReadLine()));
                        break;
                    case 4:
                        manager.GetAllBoxes(Console.WriteLine);
                        break;
                    case 5:
                        manager.PrintInnerTrees(double.Parse(Console.ReadLine()));
                        break;
                    case 6:
                        StorageSystem=false;
                        break;
                }
            }            
            Display("Thank you");           
        }
    }
}