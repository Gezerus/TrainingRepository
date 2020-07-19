using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using BoxLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesLibrary;

namespace BoxLibraryTest
{
    [TestClass]
    public class BoxTest
    {
        [TestMethod]
        public void Add_WhendDifferentShapesProvided_ShouldAddShapesCorrecttly()
        {
            //arrange
            Box box = new Box();
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(10),
                new FilmCircle(10),
            };
            //act
            foreach (Shape shape in shapes)
                box.Add(shape);
            //assert
            Assert.AreEqual(box.Count(), 3);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Add_WhendTheSameShapesProvided_ShouldThrowException()
        {
            //arrange
            Box box = new Box();
            Shape sameShape = new PaperCircle(10);
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                sameShape,
                sameShape
            };
            //act
            foreach (Shape shape in shapes)
                box.Add(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Add_WhendBoxIsFull_ShouldThrowException()
        {
            //arrange
            Box box = new Box();
            
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(10),
                new FilmEquilateralTriangle(10),
                new PaperEquilateralTriangle(5),
                new FilmCircle(10),
                new PaperCircle(10),
                new FilmEquilateralTriangle(10),
                new PaperEquilateralTriangle(5),
                new FilmCircle(10),
                new PaperCircle(10),
                new FilmEquilateralTriangle(10),
                new PaperEquilateralTriangle(5),
                new FilmCircle(10),
                new PaperCircle(10),
                new FilmEquilateralTriangle(10),
                new PaperEquilateralTriangle(5),
                new FilmCircle(10),
                new PaperCircle(10),
                new FilmEquilateralTriangle(10),
                new PaperEquilateralTriangle(5),
                new PaperEquilateralTriangle(5)
            };
            //act
            foreach (Shape shape in shapes)
                box.Add(shape);
        }

        [TestMethod]
        public void Get_CorrectIndexProvided_ShouldGetCorrectShape()
        {
            //arrange
            Box box = new Box();
            Shape firstShape = new FilmCircle(10);
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(10),
                firstShape
            };            
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            Shape SecondShape = box.Get(2);
            //assert
            Assert.AreEqual(firstShape, SecondShape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Get_WhenIndexSmallerThanZero_ShouldThrowException()
        {
            //arrange
            Box box = new Box();
            Shape firstShape = new FilmCircle(10);
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(10),
                firstShape
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            Shape SecondShape = box.Get(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Get_WhenIndexGreaterThanSizeOfBox_ShouldThrowException()
        {
            //arrange
            Box box = new Box();
            Shape firstShape = new FilmCircle(10);
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(10),
                firstShape
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            Shape SecondShape = box.Get(20);
        }

        [TestMethod]
        public void Eject_WhenCorrectIndexProvided_ShouldEjectCorrectShape()
        {
            //arrange
            Box box = new Box();
            Shape firstShape = new FilmCircle(10);
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                firstShape,
                new PaperCircle(10),                
                new PaperRectangle(34, 21)
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            Shape SecondShape = box.Eject(1);
            //assert
            Assert.AreEqual(firstShape, SecondShape);
            Assert.AreEqual(box.Count(), 3);
        }

        [TestMethod]
        public void Replace_WhenCorrectIndexProvided_ShouldReplaceCorrectShape()
        {
            //arrange
            Box box = new Box();
            Shape firstShape = new FilmCircle(10);
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperEquilateralTriangle(10),
                new PaperCircle(10),
                new PaperRectangle(34, 21)
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            box.Replace(2, firstShape);
            //assert
            Assert.AreEqual(firstShape, box.Get(2));            
        }

        [TestMethod]
        public void Find_WhenTheBoxHasProvidedShape_ShouldFindCorrectly()
        {
            //arrange
            Box box = new Box();            
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperEquilateralTriangle(10),
                new PaperCircle(10),
                new PaperRectangle(34, 21)
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            Shape result = box.FindFirstOrDefoult(new PaperCircle(10));
            //assert
            Assert.AreEqual(result, new PaperCircle(10));
        }

        [TestMethod]
        public void FindFirstOrDefoult_WhenTheBoxDoesNotHaveProvidedShape_ShouldReturnNull()
        {
            //arrange
            Box box = new Box();
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperEquilateralTriangle(10),
                new PaperCircle(10),
                new PaperRectangle(34, 21)
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            Shape result = box.FindFirstOrDefoult(new PaperCircle(15));
            //assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void GetTotalArea_ShouldCalculateAreaCorrectly()
        {
            //arrange
            Box box = new Box();
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(1),
                new PaperRectangle(10, 10),
                new PaperCircle(1),                
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            var result = box.GetTotalArea();
            //assert
            Assert.AreEqual(result, (Math.PI + Math.PI + 10 * 10));
        }

        [TestMethod]
        public void GetTotalPerimeter_ShouldCalculatePerimeterCorrectly()
        {
            //arrange
            Box box = new Box();
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(1),
                new PaperRectangle(10, 10),
                new PaperCircle(1),
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            var result = box.GetTotalPerimeter();
            //assert
            Assert.AreEqual(result, (2 * Math.PI + 2 * Math.PI + 2* 10 + 2 * 10));
        }

        [TestMethod]
        public void GetAllCircles_WhenBoxHasCircle_ShouldReturnArrayOfCircles()
        {
            //arrange
            Box box = new Box();
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(1),
                new PaperRectangle(10, 10),
                new PaperCircle(1),
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            var result = box.GetAllGircles();
            //assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0], new FilmCircle(1));
            Assert.AreEqual(result[1], new PaperCircle(1));
        }

        [TestMethod]
        public void GetAllCircles_WhenBoxDoesNotHaveCircle_ShouldReturnNull()
        {
            //arrange
            Box box = new Box();
            Shape[] shapes = new Shape[]
            {                
                new PaperRectangle(10, 10),
                new FilmEquilateralTriangle(10)                
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            var result = box.GetAllGircles();
            //assert
            Assert.AreEqual(result, null);

        }

        [TestMethod]
        public void GetAllFilmShapes_WhenBoxHasFilmShapes_ShouldReturnArrayOfFilmShapes()
        {
            //arrange
            Box box = new Box();
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(1),
                new PaperRectangle(10, 10),
                new PaperCircle(1),
            };
            foreach (Shape shape in shapes)
                box.Add(shape);
            //act
            var result = box.GetAllFilmShapes();
            //assert
            Assert.AreEqual(result.Length, 1);
            Assert.AreEqual(result[0], new FilmCircle(1));            
        }

        [TestMethod]
        public void WriteXml_ShouldWriteShapesToFileCorrectly()
        {
            //arrange
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(20),
                new FilmEquilateralTriangle(5),
                new PaperEquilateralTriangle(7),
                new FilmRectangle(10, 20),
                new PaperRectangle(20, 10)
            };

            ((IPaperShape)shapes[3]).Paint(Colors.Green);
            ((IPaperShape)shapes[5]).Paint(Colors.Yellow);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            //act
            using (XmlWriter writer = XmlWriter.Create(@"..\..\Files\testShapes.xml", settings))
            {             
                Box.WriteXml(writer, shapes);                
            }
        }

        [TestMethod]
        public void ReadXml_ShouldReadShapesFromFileCorrectly()
        {
            //arrange
            Shape[] shapes = new Shape[]
                {
                new FilmCircle(10),
                new PaperCircle(20),
                new FilmEquilateralTriangle(5),
                new PaperEquilateralTriangle(7),
                new FilmRectangle(10, 20),
                new PaperRectangle(20, 10)
                };
            ((IPaperShape)shapes[3]).Paint(Colors.Green);
            ((IPaperShape)shapes[5]).Paint(Colors.Yellow);

            Box box = new Box();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            //act
            using (XmlReader reader = XmlReader.Create(@"..\..\Files\testShapes.xml", settings))
            {
                box.ReadXml(reader);
            }


            //assert
            for (int i = 0; i < box.Count(); i++)
                Assert.AreEqual(shapes[i], box.Get(i));
        }

        [TestMethod]
        public void WriteXml_WhenUseStreamWriter_ShouldWriteShapesToFileCorrectly()
        {
            //arrange
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(20),
                new FilmEquilateralTriangle(5),
                new PaperEquilateralTriangle(7),
                new FilmRectangle(10, 20),
                new PaperRectangle(20, 10)
            };

            ((IPaperShape)shapes[3]).Paint(Colors.Green);
            ((IPaperShape)shapes[5]).Paint(Colors.Yellow);
            //act
            using (StreamWriter writer = new StreamWriter(@"..\..\Files\testShapesStreamReader.xml", false, Encoding.UTF8))
            {
                Box.WriteXml(writer, shapes);
            }
        }

        [TestMethod]
        public void ReadXml_WhenUseStreamReader_ShouldReadShapesFromFileCorrectly()
        {
            //arrange
            Shape[] shapes = new Shape[]
                {
                new FilmCircle(10),
                new PaperCircle(20),
                new FilmEquilateralTriangle(5),
                new PaperEquilateralTriangle(7),
                new FilmRectangle(10, 20),
                new PaperRectangle(20, 10)
                };

            ((IPaperShape)shapes[3]).Paint(Colors.Green);
            ((IPaperShape)shapes[5]).Paint(Colors.Yellow);

            Box box = new Box();
            //act
            using (StreamReader reader = new StreamReader(@"..\..\Files\testShapesStreamReader.xml", Encoding.UTF8))
            {
                box.ReadXml(reader);
            }
            //assert
            for (int i = 0; i < box.Count(); i++)
                Assert.AreEqual(shapes[i], box.Get(i));
        }

        [TestMethod]
        public void WriteXmlWriter_ReadStreamReader_ShouldWriteAndShapesFileCorrectly()
        {
            //arrange
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(20),
                new FilmEquilateralTriangle(5),
                new PaperEquilateralTriangle(7),
                new FilmRectangle(10, 20),
                new PaperRectangle(20, 10)
            };

            ((IPaperShape)shapes[3]).Paint(Colors.Green);
            ((IPaperShape)shapes[5]).Paint(Colors.Yellow);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            //act
            using (XmlWriter writer = XmlWriter.Create(@"..\..\Files\Shapes.xml", settings))
            {
                Box.WriteXml(writer, shapes);
            }

            Box box = new Box();
            //act
            using (StreamReader reader = new StreamReader(@"..\..\Files\Shapes.xml", Encoding.UTF8))
            {
                box.ReadXml(reader);
            }
            //assert
            for (int i = 0; i < box.Count(); i++)
                Assert.AreEqual(shapes[i], box.Get(i));
        }

        [TestMethod]
        public void WriteStreamWriter_ReadXmlReader_ShouldWriteAndReadShapesCorrectly()
        {
            //arrange
            Shape[] shapes = new Shape[]
            {
                new FilmCircle(10),
                new PaperCircle(20),
                new FilmEquilateralTriangle(5),
                new PaperEquilateralTriangle(7),
                new FilmRectangle(10, 20),
                new PaperRectangle(20, 10)
            };

            ((IPaperShape)shapes[3]).Paint(Colors.Green);
            ((IPaperShape)shapes[5]).Paint(Colors.Yellow);
            //act
            using (StreamWriter writer = new StreamWriter(@"..\..\Files\Shapes2.xml", false, Encoding.UTF8))
            {
                Box.WriteXml(writer, shapes);
            }

            Box box = new Box();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            //act
            using (XmlReader reader = XmlReader.Create(@"..\..\Files\Shapes2.xml", settings))
            {
                box.ReadXml(reader);
            }
            //assert
            for (int i = 0; i < box.Count(); i++)
                Assert.AreEqual(shapes[i], box.Get(i));
        }

    }
}
