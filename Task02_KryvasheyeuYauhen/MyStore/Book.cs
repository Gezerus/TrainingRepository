using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    /// <summary>
    /// Describes a book
    /// </summary>
    public class Book : Product
    {
        /// <summary>
        ///  initializes a book
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        public Book(string name, Money cost) : base(name, cost)
        {     
        }

        /// <summary>
        /// adds two books
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns>addition result</returns>
        public static Book operator + (Book b1, Book b2)
        {
            string name = b1.Name + " - " + b2.Name;

            var cost = (b1.Price + b2.Price) / 2;

            return new Book(name, cost);
        }

        /// <summary>
        /// converts book to integer
        /// </summary>
        /// <param name="book"></param>
        public static explicit operator int(Book book)
        {
            return book.Price;
        }

        /// <summary>
        /// convert book to double
        /// </summary>
        /// <param name="book"></param>
        public static explicit operator double(Book book)
        {
            return book.Price;
        }
        /// <summary>
        /// converts book to printer
        /// </summary>
        /// <param name="printer"></param>
        public static explicit operator Book(Printer printer)
        {
            return new Book(printer.Name, printer.Price);
        }
        /// <summary>
        /// converts book to laptop
        /// </summary>
        /// <param name="laptop"></param>
        public static explicit operator Book(Laptop laptop)
        {
            return new Book(laptop.Name, laptop.Price);
        }
    }
}
