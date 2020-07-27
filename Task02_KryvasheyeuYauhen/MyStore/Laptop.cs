using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    /// <summary>
    /// Describes a laptop
    /// </summary>
    public class Laptop : Product
    {
        /// <summary>
        ///  initializes a laptop
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        public Laptop(string name, Money cost) : base(name, cost)
        {
        }

        /// <summary>
        /// adds two laptops
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns>addition result</returns>
        public static Laptop operator +(Laptop l1, Laptop l2)
        {
            string name = l1.Name + " - " + l2.Name;

            var cost = (l1.Price + l2.Price) / 2;

            return new Laptop(name, cost);
        }

        /// <summary>
        /// converts laptop to integer
        /// </summary>
        /// <param name="laptop"></param>
        public static explicit operator int(Laptop laptop)
        {
            return laptop.Price;
        }

        /// <summary>
        /// convert laptop to double
        /// </summary>
        /// <param name="laptop"></param>
        public static explicit operator double(Laptop laptop)
        {
            return laptop.Price;
        }

        /// <summary>
        /// converts laptop to book
        /// </summary>
        /// <param name="book"></param>
        public static explicit operator Laptop(Book book)
        {
            return new Laptop(book.Name, book.Price);
        }

        /// <summary>
        /// converts laptop to printer
        /// </summary>
        /// <param name="printer"></param>
        public static explicit operator Laptop(Printer printer)
        {
            return new Laptop(printer.Name, printer.Price);
        }
    }
}
