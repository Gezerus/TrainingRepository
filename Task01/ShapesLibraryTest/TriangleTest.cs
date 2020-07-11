using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesLibrary;
using System;


namespace ShapesLibraryTest
{
    [TestClass]
    public class TriangleTest
    {
        [TestMethod]
        public void Сonstructor_WhenCorrectNumbersProvided_ShouldШnitializeObject()
        {
            //arrange and act
            var triangle = new Triangle(5, 6, 7);
            //assert
            Assert.AreEqual(triangle.A, 5);
            Assert.AreEqual(triangle.B, 6);
            Assert.AreEqual(triangle.C, 7);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Сonstructor_WhenNumbersLessThanZeroProvided_ShouldThrowExeption()
        {
            //arrange and act
            var triangle = new Triangle(-5, 6, 7);            

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Сonstructor_WhenIncorectNumbersProvided_ShouldThrowExeption()
        {
            //arrange and act
            var triangle = new Triangle(2, 3, 5);

        }

        [TestMethod]
        public void GetArea_ShouldCalculateAreaCorrectly()
        {
            //arrange
            var triamgle = new Triangle(20.5, 10, 16);
            //act and assert
            Assert.AreEqual(triamgle.GetArea(), 78.37088805002786);
        }

        [TestMethod]
        public void GetPerimeter_ShouldCalculatePerimeterCorrectly()
        {
            //arrange
            var triangle = new Triangle(5, 6, 7);
            //act and assert
            Assert.AreEqual(triangle.GetPerimeter(), 18);
        }

        [TestMethod]
        public void ToString_ShouldReturnCorrectString()
        {
            //arrange
            var triangle = new Triangle(10, 20.5, 15);
            //act and assert
            Assert.AreEqual(triangle.ToString(), "[Triangle: A = 10; B = 20,5; C = 15]");
        }

        [TestMethod]
        public void Equals_WhenObjectsAreEqual_ShouldReturnTrue()
        {
            //arrange
            var t1 = new Triangle(34, 35, 36);
            var t2 = new Triangle(34, 35, 36);
            //act and assert
            Assert.AreEqual(t1.Equals(t2), true);
        }

        [TestMethod]
        public void Equals_WhenObjectsAreNotEqual_ShouldReturnFalse()
        {
            //arrange
            var t1 = new Triangle(34, 35, 36);
            var t2 = new Triangle(34, 33, 36);
            //act and assert
            Assert.AreEqual(t1.Equals(t2), false);
        }
    }
}
