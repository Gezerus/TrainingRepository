using System;
using MathObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathObjectsTest
{
    [TestClass]
    public class VectorTest
    {
        /// <summary>
        /// Tests property Length
        /// </summary>
        [TestMethod]
        public void PropertyLength_ShouldReturnCorrectLength()
        {
            //arrange
            var vector = new Vector(5, 3, 4);
            //act and assert
            Assert.AreEqual(vector.Length, 7.0710678118654755);
        }

        /// <summary>
        /// Tests method ToString
        /// </summary>
        [TestMethod]
        public void ToString_ShouldReturnCorrectString()
        {
            //arrange
            var vector = new Vector(10.5, 5.5, 3);
            //act and assert
            Assert.AreEqual(vector.ToString(), "[10,5; 5,5; 3]");
        }

        /// <summary>
        /// Tests method Equals when given equal vectors 
        /// </summary>
        [TestMethod]
        public void Equals_WhenEqualМectorsProvided_ShouldReturnTrue()
        {
            //arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 3);
            //act and assert
            Assert.AreEqual(vector1.Equals(vector2), true);
        }

        /// <summary>
        /// Tests method Equals when given unequal vectors 
        /// </summary>
        [TestMethod]
        public void Equals_WhenUnequalМectorsProvided_ShouldReturnfalse()
        {
            //arrange
            var vector1 = new Vector(1, 2, 3);
            var vector2 = new Vector(1, 2, 4);
            //act and assert
            Assert.AreEqual(vector1.Equals(vector2), false);
        }

        /// <summary>
        /// Tests addition of vectors
        /// </summary>
        [TestMethod]
        public void VectorAddition_ShouldAddVectorsCorrectly()
        {
            //arrange
            var vector1 = new Vector(5, 3, 4);
            var vector2 = new Vector(10, 0, 3);
            //act
            var result = vector1 + vector2 + vector1;
            //assert
            Assert.AreEqual(result, new Vector(20, 6, 11));
        }

        /// <summary>
        /// Tests subtraction of vectors
        /// </summary>
        [TestMethod]
        public void VectorSubtraction_ShouldSubtractVectorsCorrectly()
        {
            //arrange
            var vector1 = new Vector(5, 3, 4);
            var vector2 = new Vector(10, 0, 3);
            //act
            var result = vector1 - vector2;
            //assert
            Assert.AreEqual(result, new Vector(-5, 3, 1));
        }

        /// <summary>
        /// Tests multiplication of vectors
        /// </summary>
        [TestMethod]
        public void VectorMultiplication_ShouldMultiplyVectorsCorrectly()
        {
            //arrange
            var vector1 = new Vector(5, 3, 4);
            var vector2 = new Vector(10, 0, 3);
            //act
            var result = vector1 * vector2;
            //assert
            Assert.AreEqual(result, new Vector(9, 25, -30));
        }

        /// <summary>
        /// Tests multiplication vector by number
        /// </summary>
        [TestMethod]
        public void MultiplyVectorByNumber_ShouldMultiplyCorrectly()
        {
            //arrange
            var vector = new Vector(5, 3, 4);            
            //act
            var result1 = vector * 2;
            var result2 = 2 * vector;
            //assert
            Assert.AreEqual(result1, new Vector(10, 6, 8));
            Assert.AreEqual(result2, new Vector(10, 6, 8));
        }

        /// <summary>
        /// Tests Division vector by number
        /// </summary>
        [TestMethod]
        public void DivisionVectorByNumber_ShouldDivideCorrectly()
        {
            //arrange
            var vector1 = new Vector(5, 3, 4);
            //act
            var result = vector1 / 2;
            //assert
            Assert.AreEqual(result, new Vector(2.5, 1.5, 2));
        }

        /// <summary>
        /// Tests method ScalarMultiply
        /// </summary>
        [TestMethod]
        public void ScalarMultiply_ShoulfMultiplyVectorsCorrectly()
        {
            //arrange
            var vector1 = new Vector(5, 3, 4);
            var vector2 = new Vector(10, 0, 3);
            //act
            var result = Vector.ScalarMultiply(vector1 , vector2);
            //assert
            Assert.AreEqual(result, new Vector(50, 0, 12));
        }
    }
}
