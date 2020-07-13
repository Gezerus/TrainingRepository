using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStore;


namespace MyStoreTest
{
    [TestClass]
    public class ProductTest
    {
       [TestMethod]
       public void AdditionProducs_WhenTheSameProducs_Provided_ShouldCalculateCorrectly()
        {
            //arange 
            Product product1 = new Product("Book", "War and Peace", 800);
            Product product2 = new Product("Book", "Idiot", 600);
            //act
            var result = product1 + product2;
            //assert
            Assert.AreEqual(result.Name, "War and Peace - Idiot");
            Assert.AreEqual((int)result.Price, 700);
        }

        [TestMethod]
        public void CastToInteger_ShouldCastCorrectle()
        {
            //arrange
            Product product = new Product("Book", "War and Peace", new Money(26, 99));
            //act
            var result = (int)product;
            //assert
            Assert.AreEqual(result, 2699);
        }

        [TestMethod]
        public void CastToDouble_ShouldCastCorrectle()
        {
            //arrange
            Product product = new Product("Book", "War and Peace", 854);
            //act
            var result = (double)product;
            //assert
            Assert.AreEqual(result, 8.54);
        }

    }
}
