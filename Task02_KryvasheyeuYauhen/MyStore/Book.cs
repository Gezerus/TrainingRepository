using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    public class Book : Product
    {
        public Book (string name, Price cost) : base( name, cost)
        {
            Type = GetType().ToString();
        }

        private Book(string name, Price cost, string type) : base(name, cost)
        {
            Type = type;
        }

        public static explicit operator Book(Laptop laptop)
        {
            return new Book(laptop.Name, new Price(laptop.Cost), laptop.Type);
        }
        
        public Book operator + (Book b1, Book b2)
        {
            string name = b1.Name + " + " + b2.Name; 
        }

    }
}
