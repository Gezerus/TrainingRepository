using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreTest
{
    [TestClass]
    public class PrinterTest
    {
        [TestMethod]
        public void Constructor_ShouldInitializesProductCorrectly()
        {
            // Arrange and act
            Printer printer = new Printer("Canon", new Money(3, 4));
            // Assert
            Assert.AreEqual(printer.Name, "Canon");
            Assert.AreEqual(printer.Price, 3.04);
        }

        [TestMethod]
        public void AddProducts_WhenCorrectProductsProvided_ShouldAddCorrectly()
        {
            // Arrange
            Printer printer1 = new Printer("Canon", new Money(3, 4));
            Printer printer2 = new Printer("HP", new Money(5, 40));
            // Act
            Printer result = printer1 + printer2;
            // Assert
            Assert.AreEqual(result.Name, "Canon - HP");
            Assert.AreEqual(result.Price, (Money)((3.04 + 5.4) / 2));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddProducts_WhenInCorrectProductsProvided_ShouldThrowException()
        {
            // Arrange
            Printer printer1 = new Printer("Canon", new Money(3, 4));
            Printer printer2 = null;
            // Act
            Printer result = printer1 + printer2;

        }

        [TestMethod]
        public void ConvertToInt_ShouldConvertCorrectly()
        {
            // Arrange
            Printer printer = new Printer("Canon", new Money(3, 4));
            // Act
            int result = (int)printer;
            // Assert
            Assert.AreEqual(result, 304);
        }

        [TestMethod]
        public void ConvertToDouble_ShouldConvertCorrectly()
        {
            // Arrange
            Printer printer = new Printer("Canon", new Money(3, 4));
            // Act
            double result = (double)printer;
            // Assert
            Assert.AreEqual(result, 3.04);
        }

        [TestMethod]
        public void ConvertToLaptop_ShouldConvertCorrectly()
        {
            // Arrange
            Printer printer = new Printer("Canon", new Money(3, 4));
            // Act
            Laptop result = (Laptop)printer;
            // Assert
            Assert.AreEqual(result.Name, "Canon");
            Assert.AreEqual(result.Price, new Money(3, 4));
        }

        [TestMethod]
        public void ConvertToBook_ShouldConvertCorrectly()
        {
            // Arrange
            Printer printer = new Printer("Canon", new Money(3, 4));
            // Act
            Book result = (Book)printer;
            // Assert
            Assert.AreEqual(result.Name, "Canon");
            Assert.AreEqual(result.Price, new Money(3, 4));
        }

    }
}
