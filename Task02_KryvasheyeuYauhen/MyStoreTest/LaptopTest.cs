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
    public class LaptopTest
    {
        [TestMethod]
        public void Constructor_ShouldInitializesProductCorrectly()
        {
            // Arrange and act
            Laptop laptop = new Laptop("Asus", new Money(3, 4));
            // Assert
            Assert.AreEqual(laptop.Name, "Asus");
            Assert.AreEqual(laptop.Price, 3.04);
        }

        [TestMethod]
        public void AddProducts_WhenCorrectProductsProvided_ShouldAddCorrectly()
        {
            // Arrange
            Laptop laptop1 = new Laptop("Asus", new Money(3, 4));
            Laptop laptop2 = new Laptop("MacBook", new Money(5, 40));
            // Act
            Laptop result = laptop1 + laptop2;
            // Assert
            Assert.AreEqual(result.Name, "Asus - MacBook");
            Assert.AreEqual(result.Price, (Money)((3.04 + 5.4) / 2));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddProducts_WhenInCorrectProductsProvided_ShouldThrowException()
        {
            // Arrange
            Laptop laptop1 = new Laptop("Asus", new Money(3, 4));
            Laptop laptop2 = null;
            // Act
            Laptop result = laptop1 + laptop2;

        }

        [TestMethod]
        public void ConvertToInt_ShouldConvertCorrectly()
        {
            // Arrange
            Laptop laptop = new Laptop("Asus", new Money(3, 4));
            // Act
            int result = (int)laptop;
            // Assert
            Assert.AreEqual(result, 304);
        }

        [TestMethod]
        public void ConvertToDouble_ShouldConvertCorrectly()
        {
            // Arrange
            Laptop laptop = new Laptop("Asus", new Money(3, 4));
            // Act
            double result = (double)laptop;
            // Assert
            Assert.AreEqual(result, 3.04);
        }

        [TestMethod]
        public void ConvertToPrinter_ShouldConvertCorrectly()
        {
            // Arrange
            Laptop laptop = new Laptop("Asus", new Money(3, 4));
            // Act
            Printer result = (Printer)laptop;
            // Assert
            Assert.AreEqual(result.Name, "Asus");
            Assert.AreEqual(result.Price, new Money(3, 4));
        }

        [TestMethod]
        public void ConvertToBook_ShouldConvertCorrectly()
        {
            // Arrange
            Laptop laptop = new Laptop("Asus", new Money(3, 4));
            // Act
            Book result = (Book)laptop;
            // Assert
            Assert.AreEqual(result.Name, "Asus");
            Assert.AreEqual(result.Price, new Money(3, 4));
        }
    }
}
