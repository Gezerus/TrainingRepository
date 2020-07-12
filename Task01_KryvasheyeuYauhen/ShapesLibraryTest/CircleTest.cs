using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ShapesLibrary;

namespace ShapesLibraryTest
{
    [TestClass]
    public class CircleTest
    {
        [TestMethod]
        public void PropertysAndСonstructor_WhenCorrectNumberProvided_ShouldШnitializeObject()
        {
            //arrange and act
            var circle = new Circle(10);
            //assert
            Assert.AreEqual(circle.Radius, 10);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PropertysAndСonstructor_WhenIncorrectNumberProvided_ShouldThrowExeption()
        {
            var circle = new Circle(-10);
        }

        [TestMethod]
        public void GetArea_ShouldCalculateAreaCorrectly()
        {
            //arrange
            var circle = new Circle(1);
            //act and assert
            Assert.AreEqual(circle.GetArea(), Math.PI);
        }

        [TestMethod]
        public void GetPerimeter_ShouldCalculatePerimeterCorrectly()
        {
            //arrange
            var circle = new Circle(5);
            //act and assert
            Assert.AreEqual(circle.GetPerimeter(), 2 * 5 * Math.PI);
        }

        [TestMethod]
        public void ToString_ShouldReturnCorrectString()
        {
            //arrange
            var circle = new Circle(10);
            //act and assert
            Assert.AreEqual(circle.ToString(), "[Circle: Radius = 10]");

        }

        [TestMethod]
        public void Equals_WhenObjectsAreEqual_ShouldReturnTrue()
        {
            //arrange
            var c1 = new Circle(15);
            var c2 = new Circle(15);
            //act and assert
            Assert.AreEqual(c1.Equals(c2), true);
        }

        [TestMethod]
        public void Equals_WhenObjectsAreNotEqual_ShouldReturnFalse()
        {
            //arrange
            var c1 = new Circle(15);
            var c2 = new Circle(20);
            //act and assert
            Assert.AreEqual(c1.Equals(c2), false);
        }
    }
}
