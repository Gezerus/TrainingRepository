using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStore;

namespace PriceTest
{
    [TestClass]
    public class PriceTest
    {
        [TestMethod]
        public void PropertyRublesAndCoins_WhenCorrectDataProvided_ShouldReturnCorrectData()
        {
            //arrange
            Price price = new Price();
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
            Price price = new Price();
            //act
            price.Rubles = -2;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PropertyCoins_WhenInCorrectDataProvided_ShouldThrowExeption()
        {
            //arrange
            Price price = new Price();
            //act
            price.Coins = 100;
        }

        [TestMethod]
        public void CastToInteger_ShouldCastCorrectle()
        {
            //arrange
            Price price = new Price(2, 56);
            //act
            var result = (int)price;
            //assert
            Assert.AreEqual(result, 256);
        }

        [TestMethod]
        public void CastToDouble_ShouldCastCorrectle()
        {
            //arrange
            Price price = new Price(2, 56);
            //act
            var result = (double)price;
            //assert
            Assert.AreEqual(result, 2.56);
        }
    }
}
