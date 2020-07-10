using System;
using GCDCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void GCD_WhenTwoPositiveParametersProvided_ShouldCalculateGCDCorectly() 
        {
            
            TimeSpan time;
            
            Assert.AreEqual(Calculator.GCD(1071, 462, out time), 21);
            Assert.AreEqual(Calculator.GCD(10711, 4625, out time), 1);
            Assert.AreEqual(Calculator.GCD(325, 1545, out time), 5);
        }

        [TestMethod]
        public void GCD_WhenThrePositiveParametersProvided_ShouldCalculateGCDCorectly()
        {
            Assert.AreEqual(Calculator.GCD(1540, 480, 320), 20);
            Assert.AreEqual(Calculator.GCD(46, 322, 482), 2);
            Assert.AreEqual(Calculator.GCD(5432, 11346, 24), 2);
        }

        [TestMethod]
        public void GCD_WhenFourPositiveParametersProvided_ShouldCalculateGCDCorectly()
        {
            Assert.AreEqual(Calculator.GCD(1540, 480, 320, 90), 10);
            Assert.AreEqual(Calculator.GCD(46, 322, 482, 560), 2);
            Assert.AreEqual(Calculator.GCD(5432, 11346, 24, 654), 2);
        }

        [TestMethod]
        public void GCD_WhenFivePositiveParametersProvided_ShouldCalculateGCDCorectly()
        {
            Assert.AreEqual(Calculator.GCD(1540, 480, 320, 90, 45), 5);
            Assert.AreEqual(Calculator.GCD(46, 322, 482, 560, 1000), 2);
            Assert.AreEqual(Calculator.GCD(5432, 11346, 24, 654, 12), 2);
        }
        [TestMethod]
        public void GCD_WhenTwoNegativeParametersProvided_ShouldCalculateGCDCorectly()
        {
            TimeSpan time;
            Assert.AreEqual(Calculator.GCD(-1071, -462, out time), 21);
            Assert.AreEqual(Calculator.GCD(-10711, -4625, out time), 1);
            Assert.AreEqual(Calculator.GCD(-325, -1545, out time), 5);
        }

        [TestMethod]
        public void GCD_WhenThreeNegativeParametersProvided_ShouldCalculateGCDCorectly()
        {
            Assert.AreEqual(Calculator.GCD(-1540, -480, -320), 20);
            Assert.AreEqual(Calculator.GCD(-46, -322, -482), 2);
            Assert.AreEqual(Calculator.GCD(-5432, -11346, -24), 2);
        }

        [TestMethod]
        public void GCD_WhenfourNegativeParametersProvided_ShouldCalculateGCDCorectly()
        {
            Assert.AreEqual(Calculator.GCD(-1540, -480, -320, -90), 10);
            Assert.AreEqual(Calculator.GCD(-46, -322, -482, -560), 2);
            Assert.AreEqual(Calculator.GCD(-5432, -11346, -24, -654), 2);
        }

        [TestMethod]
        public void GCD_WhenfiveNegativeParametersProvided_ShouldCalculateGCDCorectly()
        {
            Assert.AreEqual(Calculator.GCD(-1540, -480, -320, -90, -45), 5);
            Assert.AreEqual(Calculator.GCD(-46, -322, -482, -560, -1000), 2);
            Assert.AreEqual(Calculator.GCD(-5432, -11346, -24, -654, -12), 2);
        }

        [TestMethod]
        public void GCD_WhenTwoDifferentParametersProvided_ShouldCalculateGCDCorectlyDifferent_Numbers()
        {
            TimeSpan time;
            Assert.AreEqual(Calculator.GCD(1071, -462, out time), 21);
            Assert.AreEqual(Calculator.GCD(-10711, 4625, out time), 1);
            Assert.AreEqual(Calculator.GCD(-325, 1545, out time), 5);
        }

        [TestMethod]
        public void GCD_WhenThreeDifferentParametersProvided_ShouldCalculateGCDCorectlyDifferent_Numbers()
        {
            Assert.AreEqual(Calculator.GCD(-1540, 480, -320), 20);
            Assert.AreEqual(Calculator.GCD(46, -322, -482), 2);
            Assert.AreEqual(Calculator.GCD(-5432, -11346, 24), 2);
        }

        [TestMethod]
        public void GCD_WhenFourDifferentParametersProvided_ShouldCalculateGCDCorectlyDifferent_Numbers()
        {
            Assert.AreEqual(Calculator.GCD(-1540, -480, 320, -90), 10);
            Assert.AreEqual(Calculator.GCD(-46, 322, -482, 560), 2);
            Assert.AreEqual(Calculator.GCD(5432, -11346, -24, 654), 2);
        }

        [TestMethod]
        public void GCD_WhenFiveDifferentParametersProvided_ShouldCalculateGCDCorectlyDifferent_Numbers()
        {
            Assert.AreEqual(Calculator.GCD(1540, -480, 320, -90, -45), 5);
            Assert.AreEqual(Calculator.GCD(-46, 322, -482, 560, -1000), 2);
            Assert.AreEqual(Calculator.GCD(5432, -11346, 24, 654, -12), 2);
        }

        [TestMethod]
        public void BinaryGCD_WhenTwoPositiveParametersProvided_ShouldCalculateGCDCorectly()
        {

            TimeSpan time;

            Assert.AreEqual(Calculator.BinaryGCD(1071, 462, out time), 21);
            Assert.AreEqual(Calculator.BinaryGCD(10711, 4625, out time), 1);
            Assert.AreEqual(Calculator.BinaryGCD(325, 1545, out time), 5);
        }

        [TestMethod]
        public void BinaryGCD_WhenTwoNegativeParametersProvided_ShouldCalculateGCDCorectly()
        {
            Assert.AreEqual(Calculator.BinaryGCD(-1071, -462, out TimeSpan time), 21);
            Assert.AreEqual(Calculator.BinaryGCD(-10711, -4625, out time), 1);
            Assert.AreEqual(Calculator.BinaryGCD(-325, -1545, out time), 5);
        }

        [TestMethod]
        public void BinaryGCD_WhenTwoDifferentParametersProvided_ShouldCalculateGCDCorectlyDifferent_Numbers()
        {
            Assert.AreEqual(Calculator.BinaryGCD(1071, -462, out TimeSpan time), 21);
            Assert.AreEqual(Calculator.BinaryGCD(-10711, 4625, out time), 1);
            Assert.AreEqual(Calculator.BinaryGCD(-325, 1545, out time), 5);
        }

        [TestMethod]
        public void PrepareHistogramData_WhenCorrectArrayProvided_ShouldFormTimeIntervals()
        {
            //arrange
            int[][] numbers = new int[][]
            {
                new int[] {52, 76},
                new int[] {145, 275},
                new int[] {1890, 275},
                new int[] {18903, 2752},
                new int[] {189032, 27521},
                new int[] {1890322, 275213},
            };
            //act
            var timeIntervals = Calculator.PrepareHistogramData(numbers);
            //Assert
            Assert.AreEqual(numbers.Length, timeIntervals.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void PrepareHistogramData_WhenIncorrectArrayProvided_ShouldЕhrowException()
        {
            //arranfe
            int[][] numbers = new int[][]
            {
                new int[] {1},
                new int[] {1,2,3}
            };
            //act and Assert
            var timeIntervals = Calculator.PrepareHistogramData(numbers);   
        }

    }
   
}
