using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesLibrary;

namespace ShapesLibraryTest
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void ConstructorWithTwoParameters_WhenCorrectDataProvided_ShouldInitializePropertyCorrectly()
        {
            //arrange and act
            Rectangle rectangle = new PaperRectangle(10, 20);
            //assert
            Assert.AreEqual(rectangle.Height, 10);
            Assert.AreEqual(rectangle.Width, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorWithTwoParameters_WhenInCorrectDataProvided_ShouldThrowExeption()
        {            
            Rectangle rectangle = new PaperRectangle(0, 20);  
        }

        [TestMethod]
        public void Paint_WhenCorrectColorProvided_ShouldInicializeColorCorrectly()
        {
            //arrange
            PaperRectangle rectangle = new PaperRectangle(10, 20);
            //act
            rectangle.Paint(Colors.Green);
            //assert
            Assert.AreEqual(rectangle.Color, Colors.Green);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Paint_WhenShapeHasAlreadyPainted_ShouldThrowExeption()
        {
            //arrange
            PaperRectangle rectangle = new PaperRectangle(10, 20);
            //act
            rectangle.Paint(Colors.Green);
            rectangle.Paint(Colors.Red);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Paint_WhenWhiteColorProvided_ShouldThrowExeption()
        {
            //arrange
            PaperRectangle rectangle = new PaperRectangle(10, 20);
            //act
            rectangle.Paint(Colors.White);
            
        }

        [TestMethod]
        public void ConstructorWithThreeParameters_WhenCorrectDataProvided_ShouldInitializePropertyCorrectly()
        {
            //arrange
            PaperRectangle targetRectangle = new PaperRectangle(11, 21);
            targetRectangle.Paint(Colors.Red);
            //act
            Rectangle rectangle = new PaperRectangle(10, 20, targetRectangle);
            //assert
            Assert.AreEqual(rectangle.Height, 10);
            Assert.AreEqual(rectangle.Width, 20);
            Assert.AreEqual(((IPaperShape)rectangle).Color, Colors.Red);
        }

        [TestMethod] 
        public void PaperRectangleToString_ShouldReturnCorrectString()
        {
            //arrange
            PaperRectangle targetRectangle = new PaperRectangle(11, 21);
            targetRectangle.Paint(Colors.Red);
            //act and assert
            Assert.AreEqual(targetRectangle.ToString(), "[PaperRectangle: Height = 11; Width = 21; Color = Red]");
        }

        [TestMethod]
        public void FilmRectangleToString_ShouldReturnCorrectString()
        {
            //arrange
            FilmRectangle targetRectangle = new FilmRectangle(11, 21);            
            //act and assert
            Assert.AreEqual(targetRectangle.ToString(), "[FilmRectangle: Height = 11; Width = 21]");
        }

        [TestMethod]
        public void Equals_WhenEqualRectangleProvided_ShouldReturnTrue()
        {
            //arrange
            FilmRectangle filmRectangle1 = new FilmRectangle(10, 20);
            FilmRectangle filmRectangle2 = new FilmRectangle(10, 20);
            PaperRectangle paperRectangle1 = new PaperRectangle(10, 20);
            PaperRectangle paperRectangle2 = new PaperRectangle(10, 20);
            //act and assert
            Assert.AreEqual(filmRectangle1.Equals(filmRectangle2), true);
            Assert.AreEqual(paperRectangle1.Equals(paperRectangle2), true);
        }

        [TestMethod]
        public void Equals_WhenUnequalRectangleProvided_ShouldReturnFalse()
        {
            //arrange
            FilmRectangle filmRectangle1 = new FilmRectangle(10, 20);            
            PaperRectangle paperRectangle1 = new PaperRectangle(10, 20);
            PaperRectangle paperRectangle2 = new PaperRectangle(10, 5);
            //act and assert
            Assert.AreEqual(filmRectangle1.Equals(paperRectangle1), false);
            Assert.AreEqual(paperRectangle1.Equals(paperRectangle2), false);
        }

        [TestMethod]
        public void GetArea_ShouldCalculateAreaCorrectly()
        {
            //arrange
            Rectangle rectangle= new FilmRectangle(10, 2);
            //act and assert
            Assert.AreEqual(rectangle.GetArea(), 20);
        }

        [TestMethod]
        public void GetPerimeter_ShouldCalculatePerimeterCorrectly()
        {
            //arrange
            Rectangle rectangle = new FilmRectangle(10, 2);
            //act and assert
            Assert.AreEqual(rectangle.GetPerimeter(), 24);
        }

        [TestMethod]
        public void WriteXml_ShouldWriteRectangleToFileCorrectly()
        {
            //arrange
            Shape testrectangle1 = new FilmRectangle(10, 5);
            Shape testrectangle2 = new PaperRectangle(200, 150);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            //act
            using (XmlWriter writer = XmlWriter.Create(@"..\..\Files\testRectangle.xml", settings))
            {
                testrectangle1.WriteXml(writer);
                testrectangle2.WriteXml(writer);
            }
        }

        [TestMethod]
        public void ReadXml_ShouldReadCircleFromFileCorrectly()
        {
            //arrange
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            Shape rectangle1;
            Shape rectangle2;
            //act
            using (XmlReader reader = XmlReader.Create(@"..\..\Files\testRectangle.xml", settings))
            {
                rectangle1 = new FilmRectangle(reader);
                rectangle2 = new PaperRectangle(reader);
            }
            //assert
            Assert.AreEqual(rectangle1, new FilmRectangle(10, 5));
            Assert.AreEqual(rectangle2, new PaperRectangle(200, 150));
        }

        [TestMethod]
        public void WriteXml_WhenUseStreamWriter_ShouldWriteRectangleToFileCorrectly()
        {
            //arrange
            Shape testrectangle1 = new FilmRectangle(10, 5);
            Shape testrectangle2 = new PaperRectangle(200, 150);

            //act
            using (StreamWriter writer = new StreamWriter(@"..\..\Files\testRectangleStreamReader.xml", false, Encoding.UTF8))
            {
                testrectangle1.WriteXml(writer);
                testrectangle2.WriteXml(writer);
            }
        }

        [TestMethod]
        public void ReadXml_WhenUseStreamReader_ShouldReadCircleFromFileCorrectly()
        {
            //arrange
            Shape rectangle1;
            Shape rectangle2;
            //act
            using (StreamReader reader = new StreamReader(@"..\..\Files\testRectangleStreamReader.xml", Encoding.UTF8))
            {
                reader.ReadLine();
                rectangle1 = new FilmRectangle(reader);
                reader.ReadLine();
                rectangle2 = new PaperRectangle(reader);
            }
            //assert
            Assert.AreEqual(rectangle1, new FilmRectangle(10, 5));
            Assert.AreEqual(rectangle2, new PaperRectangle(200, 150));
        }
    }
}
