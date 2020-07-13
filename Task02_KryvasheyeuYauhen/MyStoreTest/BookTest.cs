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
        public void ConstructorBook_WhenBookProvided_ShouldInitializeTypeCorrectly()
        {
            //arrange and act
            Book book1 = new Book("Space", new Price(2, 34));
            Product book2 = new Book("Moon", new Price(3, 35));
            //assert
            Assert.AreEqual(book1.Type, "MyStore.Book");
            Assert.AreEqual(book2.Type, "MyStore.Book");
        }

        [TestMethod]
        public void ExplicitCastBookToLaploo_WhenLaptopProvided_ShouldReturnBook()
        {
            //arrange
            Book book = new Book("Space", new Price(2, 34));
            Laptop laptop = new Laptop("Lenovo", new Price(3, 35));
            //act
            book = (Book)laptop;
            //assert
            Assert.AreEqual(book.Type, "MyStore.Laptop");            
        }

    }
}
