using System;
using System.Collections;
using System.Collections.Generic;
namespace Customer_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Customers customers = new Customers();
            customers.Add(new Customer
            {
                Name = "Valod",
                City = "Yerevan",
                Mobile = "+374 11 111111"
            });
            customers.Add(new Customer
            {
                Name = "Garnik",
                City = "Vanadzor",
                Mobile = "+374 22 222222"
            });
            foreach (Customer item in customers)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
    public class Customers : IEnumerable
    {
        static readonly Customer[] _emptyArray = new Customer[0];
        private const int _defaultCapacity = 0;
        private int _size;
        private int _version;
        private Customer[] _items;
        public Customers()
        {
            _items = new Customer[_defaultCapacity];
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        Customer[] newItems = new Customer[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }
        }
        public void Add(Customer item)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size++] = item;
            _version++;
        }
        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > 0X7FEFFFFF) newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new CustomerIEnumerator(_items, _items.Length);
        }
    }
    public class Customer
    {
        private String _Name;
        private String _City;
        private String _Mobile;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        public String Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public override string ToString() => $"Name is: {Name}\nLive in: {City}\nMobile number is: {Mobile}";
    }
    public class CustomerIEnumerator : IEnumerator
    {
        public CustomerIEnumerator(Customer[] source, int size)
        {
            _source = source;
            _size = size;
        }
        int _size;
        int _count = 0;
        Customer[] _source;
        public object Current => _source[_count++];
        public bool MoveNext()
        {
            return _count < _size;
        }
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
