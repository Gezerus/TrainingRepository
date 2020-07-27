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
    public class BookTest
    {
        [TestMethod]
        public void Constructor_ShouldInitializesProductCorrectly()
        {
            // Arrange and act
            Book book = new Book("War and pice", new Money(3, 4));
            // Assert
            Assert.AreEqual(book.Name, "War and pice");
            Assert.AreEqual(book.Price, 3.04);
        }

        [TestMethod]
        public void AddProducts_WhenCorrectProductsProvided_ShouldAddCorrectly()
        {
            // Arrange
            Book book1 = new Book("War and pice", new Money(3, 4));
            Book book2 = new Book("Idiot", new Money(5, 40));
            // Act
            Book result = book1 + book2;
            // Assert
            Assert.AreEqual(result.Name, "War and pice - Idiot");
            Assert.AreEqual(result.Price, (Money)((3.04 + 5.4)/2));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddProducts_WhenInCorrectProductsProvided_ShouldThrowException()
        {
            // Arrange
            Book book1 = new Book("War and pice", new Money(3, 4));
            Book book2 = null;
            // Act
            Book result = book1 + book2;
            
        }

        [TestMethod]
        public void ConvertToInt_ShouldConvertCorrectly()
        {
            // Arrange
            Book book = new Book("War and pice", new Money(3, 4));
            // Act
            int result = (int)book;
            // Assert
            Assert.AreEqual(result, 304);
        }

        [TestMethod]
        public void ConvertToDouble_ShouldConvertCorrectly()
        {
            // Arrange
            Book book = new Book("War and pice", new Money(3, 4));
            // Act
            double result = (double)book;
            // Assert
            Assert.AreEqual(result, 3.04);
        }

        [TestMethod]
        public void ConvertToPrinter_ShouldConvertCorrectly()
        {
            // Arrange
            Book book = new Book("War and pice", new Money(3, 4));
            // Act
            Printer result = (Printer)book;
            // Assert
            Assert.AreEqual(result.Name,"War and pice");
            Assert.AreEqual(result.Price, new Money(3, 4));
        }

        [TestMethod]
        public void ConvertToLaptop_ShouldConvertCorrectly()
        {
            // Arrange
            Book book = new Book("War and pice", new Money(3, 4));
            // Act
            Laptop result = (Laptop)book;
            // Assert
            Assert.AreEqual(result.Name, "War and pice");
            Assert.AreEqual(result.Price, new Money(3, 4));
        }
    }
}
