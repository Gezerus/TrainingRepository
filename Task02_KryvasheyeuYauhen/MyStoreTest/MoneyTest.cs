using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStore;

namespace MoneyTest
{
    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void PropertyRublesAndCoins_WhenCorrectDataProvided_ShouldReturnCorrectData()
        {
            //arrange
            Money price = new Money();
            //act
            price.Rubles = 2;
            price.Coins = 34;
            //assert
            Assert.AreEqual($"{price.Rubles} {price.Coins}", "2 34");            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PropertyRubles_WhenInCorrectDataProvided_ShouldThrowExeption()
        {
            //arrange
            Money price = new Money();
            //act
            price.Rubles = -2;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PropertyCoins_WhenInCorrectDataProvided_ShouldThrowExeption()
        {
            //arrange
            Money price = new Money();
            //act
            price.Coins = 100;
        }

        [TestMethod]
        public void CastToInteger_ShouldCastCorrectle()
        {
            //arrange
            Money price = new Money(2, 56);
            //act
            var result = (int)price;
            //assert
            Assert.AreEqual(result, 256);
        }

        [TestMethod]
        public void CastToDouble_ShouldCastCorrectle()
        {
            //arrange
            Money price = new Money(2, 06);
            //act
            var result = (double)price;
            //assert
            Assert.AreEqual(result, 2.06);
        }

        [TestMethod]
        public void CastFromDouble_ShouldCastCorrectle()
        {
            //arrange
            var d1 = 3.95123123;
            //act
            var result = (Money)d1;
            //assert
            Assert.AreEqual($"{result.Rubles} {result.Coins}", "3 95");
        }

        [TestMethod]
        public void AdditionMoney_ShouldAddCorrectly()
        {
            //arrange
            var m1 = new Money(3, 95);
            var m2 = new Money(2, 7);
            //act 
            var result = m1 + m2;
            //assert
            Assert.AreEqual($"{result.Rubles} {result.Coins}", "6 2");
        }


    }
}