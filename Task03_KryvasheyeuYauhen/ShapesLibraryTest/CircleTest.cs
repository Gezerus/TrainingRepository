﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapesLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShapesLibraryTest
{
    [TestClass]
    public class CircleTest
    {
        [TestMethod]
        public void ConstructorWithOneParameter_WhenCorrectDataProvided_ShouldInitializePropertyCorrectly()
        {
            //arrange and act
            Circle circle = new PaperCircle(10);
            //assert
            Assert.AreEqual(circle.Radius, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorWithOneParameter_WhenInCorrectDataProvided_ShouldThrowExeption()
        {
            Circle circle = new PaperCircle(0);
        }

        [TestMethod]
        public void ConstructorWithTwoParameters_WhenCorrectDataProvided_ShouldInitializePropertyCorrectly()
        {
            //arrange
            PaperRectangle targetRectangle = new PaperRectangle(11, 21);
            targetRectangle.Paint(Colors.Red);
            //act
            Circle circle = new PaperCircle(5, targetRectangle);
            //assert
            Assert.AreEqual(circle.Radius, 5);            
            Assert.AreEqual(((IPaperShape)circle).Color, Colors.Red);
        }

        [TestMethod]
        public void PaperCircleToString_ShouldReturnCorrectString()
        {
            //arrange
            PaperCircle targetCircle = new PaperCircle(11);
            targetCircle.Paint(Colors.Red);
            //act and assert
            Assert.AreEqual(targetCircle.ToString(), "[PaperCircle: Radius = 11; Color = Red]");
        }

        [TestMethod]
        public void FilmCircleToString_ShouldReturnCorrectString()
        {
            //arrange
            FilmCircle targetCircle = new FilmCircle(11);
            //act and assert
            Assert.AreEqual(targetCircle.ToString(), "[FilmCircle: Radius = 11]");
        }

        [TestMethod]
        public void Equals_WhenEqualCircleProvided_ShouldReturnTrue()
        {
            //arrange
            FilmCircle filmCircle1 = new FilmCircle(10);
            FilmCircle filmCircle2 = new FilmCircle(10);
            PaperCircle paperCircle1 = new PaperCircle(10);
            PaperCircle paperCircle2 = new PaperCircle(10);
            //act and assert
            Assert.AreEqual(filmCircle1.Equals(filmCircle2), true);
            Assert.AreEqual(paperCircle1.Equals(paperCircle2), true);
        }

        [TestMethod]
        public void Equals_WhenUnequalCircleProvided_ShouldReturnFalse()
        {
            //arrange
            FilmCircle filmCircle1 = new FilmCircle(10);
            PaperCircle paperCircle1 = new PaperCircle(10);
            PaperCircle paperCircle2 = new PaperCircle(15);
            //act and assert
            Assert.AreEqual(filmCircle1.Equals(paperCircle1), false);
            Assert.AreEqual(filmCircle1.Equals(paperCircle2), false);
        }

        [TestMethod]
        public void GetArea_ShouldCalculateAreaCorrectly()
        {
            //arrange
            Circle circle = new FilmCircle(1);
            //act and assert
            Assert.AreEqual(circle.GetArea(), Math.PI);
        }

        [TestMethod]
        public void GetPerimeter_ShouldCalculatePerimeterCorrectly()
        {
            //arrange
            Circle circle = new FilmCircle(1);
            //act and assert
            Assert.AreEqual(circle.GetPerimeter(), 2 * Math.PI);
        }

        [TestMethod]
        public void WriteXml_ShouldWriteCircleToFileCorrectly()
        {
            //arrange
            Shape testCircle1 = new FilmCircle(10);
            Shape testCircle2 = new PaperCircle(200);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            //act
            using(XmlWriter writer = XmlWriter.Create(@"..\..\Files\testCircle.xml", settings))
            {
                testCircle1.WriteXml(writer);
                testCircle2.WriteXml(writer);
            }
        }

        [TestMethod]
        public void ReadXml_ShouldReadCircleFromFileCorrectly()
        {
            //arrange
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;
            Shape circle1;
            Shape circle2;
            //act
            using (XmlReader reader = XmlReader.Create(@"..\..\Files\testCircle.xml", settings))
            {
                circle1 = new FilmCircle(reader);
                circle2 = new PaperCircle(reader);
            }
            //assert
            Assert.AreEqual(circle1, new FilmCircle(10));
            Assert.AreEqual(circle2, new PaperCircle(200));
        }

        [TestMethod]
        public void WriteXml_WhenUseStreamWriter_ShouldWriteCircleToFileCorrectly()
        {
            //arrange
            Shape testCircle1 = new FilmCircle(10);
            Shape testCircle2 = new PaperCircle(200);
            //act
            using (StreamWriter writer = new StreamWriter(@"..\..\Files\testCircleStreamReader.xml",false, Encoding.UTF8))
            {
                testCircle1.WriteXml(writer);
                testCircle2.WriteXml(writer);
            }
        }

        [TestMethod]
        public void ReadXml_WhenUseStreamReader_ShouldReadCircleFromFileCorrectly()
        {
            //arrange
            Shape circle1;
            Shape circle2;
            //act
            using (StreamReader reader = new StreamReader(@"..\..\Files\testCircleStreamReader.xml", Encoding.UTF8))
            {
                reader.ReadLine();
                circle1 = new FilmCircle(reader);
                reader.ReadLine();
                circle2 = new PaperCircle(reader);
            }
            //assert
            Assert.AreEqual(circle1, new FilmCircle(10));
            Assert.AreEqual(circle2, new PaperCircle(200));
        }
    }
}
