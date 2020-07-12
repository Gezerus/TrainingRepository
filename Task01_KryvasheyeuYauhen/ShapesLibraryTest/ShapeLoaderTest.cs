using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapesLibrary;
using System.IO;

namespace ShapesLibraryTest
{
    [TestClass]
    public class ShapeLoaderTest
    {
        [TestMethod]
        public void Load_WhenCorrectShapesFileProvided_ShouldReturnShapesArray()
        {
            //arrange
            string path = @"..\..\Files\Shapes.txt";

            var shapes = new Shape[]
            {
                new Triangle(10, 12, 11),
                new Rectangle(25, 30),
                new Circle(30),
                new Rectangle(10.5, 20.5)
            };
            //Act
            var result = ShapeLoader.Load(path);
            //Assert
            for (int i = 0; i < result.Length; i++)
                Assert.IsTrue(result[i].Equals(shapes[i]));            
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Load_WhenFileDoesNotExist_ShouldTrow_Exeption()
        {
            //arrange
            string path = @"..\..\Files\NonexistentFile.txt";
            //act
            var result = ShapeLoader.Load(path);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Load_WhenFileIsEmpty_ShouldTrow_Exeption()
        {
            //arrange
            string path = @"..\..\Files\EmptyFile.txt";
            //act
            var result = ShapeLoader.Load(path);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Load_WhenFileIsIncorrect_ShouldTrow_Exeption()
        {
            //arrange
            string path = @"..\..\Files\IncorrectFile.txt";
            //act
            var result = ShapeLoader.Load(path);
        }
    }
}
