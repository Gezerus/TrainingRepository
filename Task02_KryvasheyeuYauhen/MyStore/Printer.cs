using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{

    /// <summary>
    /// Describes a printer
    /// </summary>
    public class Printer : Product
    {
        /// <summary>
        ///  initializes a printer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        public Printer(string name, Money cost) : base(name, cost)
        {
        }

        /// <summary>
        /// adds two printers
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>addition result</returns>
        public static Printer operator +(Printer p1, Printer p2)
        {
            string name = p1.Name + " - " + p2.Name;

            var cost = (p1.Price + p2.Price) / 2;

            return new Printer(name, cost);
        }

        /// <summary>
        /// converts printer to integer
        /// </summary>
        /// <param name="printer"></param>
        public static explicit operator int(Printer printer)
        {
            return printer.Price;
        }

        /// <summary>
        /// converts printer to double
        /// </summary>
        /// <param name="printer"></param>
        public static explicit operator double(Printer printer)
        {
            return printer.Price;
        }
        /// <summary>
        /// converts printer to book
        /// </summary>
        /// <param name="book"></param>
        public static explicit operator Printer(Book book)
        {
            return new Printer(book.Name, book.Price);
        }

        /// <summary>
        /// converts printer to laptop
        /// </summary>
        /// <param name="laptop"></param>
        public static explicit operator Printer(Laptop laptop)
        {
            return new Printer(laptop.Name, laptop.Price);
        }

    }
}
