using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesLibrary;

namespace ShapesLibraryTest
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void PropertysAndСonstructor_WhenCorrectNumbersProvided_ShouldШnitializeObject()
        {
            //arrange and act
            var rectangle = new Rectangle(5, 6);
            //assert
            Assert.AreEqual(rectangle.Height, 5);
            Assert.AreEqual(rectangle.Width, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PropertysAndСonstructor_WhenIncorrectNumbersProvided_ShouldThrowExeption()
        {            
            var rectangle = new Rectangle(-5, 0);            
        }

        [TestMethod]
        public void GetArea_ShouldCalculateAreaCorrectly()
        {
            //arrange
            var rectangle = new Rectangle(5, 6);   
            //act and assert
            Assert.AreEqual(rectangle.GetArea(), 30);            
        }

        [TestMethod]
        public void GetPerimeter_ShouldCalculatePerimeterCorrectly()
        {
            //arrange
            var rectangle = new Rectangle(5, 6);
            //act and assert
            Assert.AreEqual(rectangle.GetPerimeter(), 22);
        }

        [TestMethod]
        public void ToString_ShouldReturnCorrectString()
        {
            //arrange
            var rectangle = new Rectangle(10, 20.5);
            //act and assert
            Assert.AreEqual(rectangle.ToString(), "[Rectangle: Height = 10; Width = 20,5]");
           
        }

        [TestMethod]
        public void Equals_WhenObjectsAreEqual_ShouldReturnTrue()
        {
            //arrange
            var r1 = new Rectangle(34, 35);
            var r2 = new Rectangle(34, 35);
            //act and assert
            Assert.AreEqual(r1.Equals(r2), true);
        }

        [TestMethod]
        public void Equals_WhenObjectsAreNotEqual_ShouldReturnFalse()
        {
            //arrange
            var r1 = new Rectangle(34, 35);
            var r2 = new Rectangle(35, 35);
            //act and assert
            Assert.AreEqual(r1.Equals(r2), false);
        }
    }
}
