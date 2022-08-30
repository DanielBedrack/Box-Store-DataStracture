using BoxStore.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxStore.Model
{
    public class Box
    {
        private readonly double _width;
        private readonly double _height;
        private int _amount;
        private DateTime _date;
        private DateTime _expireDate;
        private int _maxAmount = Config.Data.MaxBoxes;
        private int _minAmount = Config.Data.MinBoxes;
        private QueueNode<Box> _boxQueue;

        public int Amount { get { return _amount; } }
        public DateTime Date { get { return _date; } }
        public double Width { get { return _width; } }
        public double Height { get { return _height; } }
        public int MaxAmount { get { return _maxAmount; } }
        public DateTime ExpireDate { get { return _expireDate; } }
        public QueueNode<Box> BoxQueue { get { return _boxQueue; } }

        public Box(double width, double height, int amount, DateTime date)
        {
            if (width <= 0)
                width = 1;
            if (height <= 0)
                height = 1;
            _width = width;
            _height = height;
            _amount = amount;
            _date = date;
            _expireDate= _date.AddSeconds(Config.Data.Expired);
            _boxQueue = new QueueNode<Box>(this);
        }
        public Box(double width, double height, int amount) : this(width, height, amount, DateTime.Now)
        {
        }
        public Box() { }
        /// <summary>
        /// Managing amount request
        /// </summary>
        /// <param name="amountRequest"></param>
        /// <returns></returns>
        public int RequestBox(int amountRequest)
        {
            _date = DateTime.Now;
            if (_amount > 0)
            {
                _amount -= amountRequest;
            }

            if (_amount <= _minAmount && _amount > 0)
            {
                Console.WriteLine($"Left In Storage: {_amount}");
            }
            if (_amount <= 0)
            {
                Console.WriteLine("Not enough boxes of this size. Missing: " + (_amount * -1));
                return _amount * -1;
            }
            else
                return 0;
        }
        /// <summary>
        /// Managing amount
        /// </summary>
        /// <param name="amount"></param>
        public void FillBoxes(int amount)
        {
            _date= DateTime.Now;
            if (_amount + amount > MaxAmount)
            {               
                Console.WriteLine($"You reached to your Max amount {_maxAmount}. we add {MaxAmount - _amount}, so now amount is {MaxAmount}.");
                _amount = MaxAmount;
            }
            else
                _amount += amount;
        }
        public override string ToString()
        {
            return $"Box's width: {_width}. Box's height: {_height}. Current amount of the boxs: {_amount}. Last time box was requested: {_date:g}";
        }

    }
}
