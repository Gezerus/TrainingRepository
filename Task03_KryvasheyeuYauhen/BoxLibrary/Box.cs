using ShapesLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BoxLibrary
{
    public class Box 
    {
        private const int _size = 20;

        private Shape[] _shapes;

        public Box()
        {
            _shapes = new Shape[_size];
        }

        /// <summary>
        /// Сounts ырфзуы in еру box
        /// </summary>
        /// <returns>number of shepes</returns>
        public int Count()
        {
            int counter = 0;

            foreach (Shape shape in _shapes)
                if (shape != null)
                    counter++;
            return counter;
        }
        /// <summary>
        /// Adds a shape in the box
        /// </summary>
        /// <param name="shape"></param>
        public void Add(Shape shape)
        {
            if (Count() >= _size)
                throw new ArgumentOutOfRangeException("shape","The box is full");
            int i;
            for (i = 0; (i < _size) && (_shapes[i] != null); i++)
                if (_shapes[i] == shape)
                    throw new Exception("This shape has already been added in the box");
            if (i == _size)
                i--;
            _shapes[i] = shape;
        }
        /// <summary>
        /// Get a shame from the box
        /// </summary>
        /// <param name="index"> The index of the shape</param>
        /// <returns>The shape</returns>
        public Shape Get(int index)
        {
            if ((index >= Count()) || (index < 0))
                throw new ArgumentOutOfRangeException("index", "The index cann't be smaller then zero and greater than number of shapes in the box");
            return _shapes[index];
        }

        /// <summary>
        /// Ejects a shape from the box
        /// </summary>
        /// <param name="index">The index of the shape</param>
        /// <returns></returns>
        public Shape Eject(int index)
        {
            if ((index >= Count()) || (index < 0))
                throw new ArgumentOutOfRangeException("index", "The index cann't be smaller then zero and greater than number of shapes in the box");
            Shape result = _shapes[index];
            _shapes[index] = null;

            //shift null to end 
            for (int i = index; (i < (_size - 1)) && (_shapes[i+1] != null); i++)
            {
                Shape temp = _shapes[i + 1];
                _shapes[i + 1] = _shapes[i];
                _shapes[i] = temp;
            }
            return result;
        }

        /// <summary>
        /// Replaces a shape in the box
        /// </summary>
        /// <param name="index"></param>
        /// <param name="shape"></param>
        public void Replace(int index, Shape shape)
        {
            if ((index >= Count()) || (index < 0))
                throw new ArgumentOutOfRangeException("index", "The index cann't be smaller then zero and greater than number of shapes in the box");
            _shapes[index] = shape;
        }

        /// <summary>
        /// Finds the same shape or returns null if the shape isnot found
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public Shape FindFirstOrDefoult(Shape shape)
        {
            for(int i = 0; i< Count(); i++)
                if (_shapes[i].Equals(shape))
                    return _shapes[i];
            return null;
        }

        /// <summary>
        /// Calculates the area of the all shapes in the box
        /// </summary>
        /// <returns>total area</returns>
        public double GetTotalArea()
        {
            double total = 0;

            for (int i = 0; i < Count(); i++)
                total += _shapes[i].GetArea();
            return total;
        }
        /// <summary>
        /// Calculates the perimeter of the all shapes in the box
        /// </summary>
        /// <returns>total perimeter</returns>
        public double GetTotalPerimeter()
        {
            double total = 0;

            for (int i = 0; i < Count(); i++)
                total += _shapes[i].GetPerimeter();
            return total;
        }

        /// <summary>
        /// Get all circles from the box
        /// </summary>
        /// <returns>the array of circles or null if there are not circles</returns>
        public Shape[] GetAllGircles()
        {
            int circleCounter = 0;

            for(int i = 0; i < Count(); i++)
                if (_shapes[i] is Circle)
                    circleCounter++;

            if (circleCounter == 0)
                return null;

            Shape[] result = new Shape[circleCounter];

            int j = 0;
            for(int i = 0; i < Count(); i++)
            {
                
                if(_shapes[i] is Circle)
                    result[j++] = _shapes[i];
            }

            return result;
        }

        /// <summary>
        /// Get all film shapes from the box
        /// </summary>
        /// <returns>the array of film shapes or null if there are not film shapes</returns>
        public Shape[] GetAllFilmShapes()
        {
            int filmShapesCounter = 0;

            for (int i = 0; i < Count(); i++)
                if (!(_shapes[i] is IPaperShape))
                    filmShapesCounter++;

            if (filmShapesCounter == 0)
                return null;

            Shape[] result = new Shape[filmShapesCounter];

            int j = 0;
            for (int i = 0; i < Count(); i++)
            {

                if (!(_shapes[i] is IPaperShape))
                    result[j++] = _shapes[i];
            }

            return result;
        }

        /// <summary>
        /// Get all paper shapes from the box
        /// </summary>
        /// <returns>the array of paper shapes or null if there are not paper shapes</returns>
        public Shape[] GetAllPaperShapes()
        {
            int filmShapesCounter = 0;

            for (int i = 0; i < Count(); i++)
                if (_shapes[i] is IPaperShape)
                    filmShapesCounter++;

            if (filmShapesCounter == 0)
                return null;

            Shape[] result = new Shape[filmShapesCounter];

            int j = 0;
            for (int i = 0; i < Count(); i++)
            {

                if (_shapes[i] is IPaperShape)
                    result[j++] = _shapes[i];
            }

            return result;
        }

        /// <summary>
        /// Get all shapes from the box
        /// </summary>
        /// <returns>the array of shapes or null if there are not shapes</returns>
        public Shape[] GetAll()
        {
            if (Count() == 0)
                return null;

            Shape[] result = new Shape[Count()];

            for (int i = 0; i < Count(); i++)
                result[i] = _shapes[i];
            return result;
        }
        /// <summary>
        /// Writes array of shapes to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="shapes"></param>
        public static void WriteXml(XmlWriter writer, Shape[] shapes)
        {
            if (shapes.Length > _size)
                throw new Exception(string.Format("it is possible to write no more than {0} shapes", _size));
            writer.WriteStartDocument();
            writer.WriteStartElement("Shapes");
            foreach (Shape shape in shapes)
                shape.WriteXml(writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Reads array of shapes from Xml file and save their in box using XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            reader.Read();
            reader.Read();
            while (reader.NodeType == XmlNodeType.Element)
            {
                switch(reader.Name)
                {
                    case "filmcircle" :
                        Add(new FilmCircle(reader));
                        break;
                    case "papercircle":
                        Add(new PaperCircle(reader));
                        break;
                    case "filmequilateraltriangle":
                        Add(new FilmEquilateralTriangle(reader));
                        break;
                    case "paperequilateraltriangle":
                        Add(new PaperEquilateralTriangle(reader));
                        break;
                    case "filmrectangle":
                        Add(new FilmRectangle(reader));
                        break;
                    case "paperrectangle":
                        Add(new PaperRectangle(reader));
                        break;
                    default :
                        throw new XmlException(string.Format("Unexpected node: {0}", reader.Name));                        
                }                
            }
        }

        /// <summary>
        /// Writes array of shapes to Xml file using StreamWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="shapes"></param>
        public static void WriteXml(StreamWriter writer, Shape[] shapes)
        {
            if (shapes.Length > _size)
                throw new Exception(string.Format("it is possible to write no more than {0} shapes", _size));
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            writer.WriteLine("<Shapes>");
            foreach (Shape shape in shapes)
                shape.WriteXml(writer);
            writer.WriteLine("</Shapes>");
        }

        /// <summary>
        /// Reads array of shapes from Xml file and save their in box using StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(StreamReader reader)
        {
            string tempString;
            reader.ReadLine();
            reader.ReadLine();

            while ((tempString = reader.ReadLine()) != "</Shapes>")
            {
                switch (tempString.Trim(new char[] { ' ', '<', '>'}))
                {
                    case "filmcircle":
                        Add(new FilmCircle(reader));
                        break;
                    case "papercircle":
                        Add(new PaperCircle(reader));
                        break;
                    case "filmequilateraltriangle":
                        Add(new FilmEquilateralTriangle(reader));
                        break;
                    case "paperequilateraltriangle":
                        Add(new PaperEquilateralTriangle(reader));
                        break;
                    case "filmrectangle":
                        Add(new FilmRectangle(reader));
                        break;
                    case "paperrectangle":
                        Add(new PaperRectangle(reader));
                        break;
                    default:
                        throw new XmlException(string.Format("Unexpected node: {0}", tempString));
                }
            }
        }


    }

}
