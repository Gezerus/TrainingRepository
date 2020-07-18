using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ShapesLibrary
{
    /// <summary>
    /// abstract class for all shapes
    /// </summary>
    public abstract class Shape
    {
        public  Shape()
        { }
        /// <summary>
        /// initializes the shape with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public Shape(XmlReader reader)
        {
            ReadXml(reader);
        }

        /// <summary>
        /// initializes the shape with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public Shape(StreamReader reader)
        {
            ReadXml(reader);
        }

        /// <summary>
        /// Reads and initializes the shape with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public abstract void ReadXml(XmlReader reader);

        /// <summary>
        /// Writes the shape to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        public abstract void WriteXml(XmlWriter writer);

        /// <summary>
        /// Reads and initializes the shape with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public abstract void ReadXml(StreamReader reader);

        /// <summary>
        /// Writes the shape to Xml file using StreamWriter
        /// </summary>
        /// <param name="writer"></param>
        public abstract void WriteXml(StreamWriter writer);

        /// <summary>
        /// Calculateы the area of ​​the Shape
        /// </summary>
        /// <returns>The figure area</returns>
        public abstract double GetArea();

        /// <summary>
        /// Calculateы the perimeter of the Shape
        /// </summary>
        /// <returns>The figure Perimeter</returns>
        public abstract double GetPerimeter();
    }

    /// <summary>
    /// describes a shape made of paper
    /// </summary>
    public interface IPaperShape
    {
        public Colors Color { get; }

        public void Paint(Colors color);

    }
    public enum Colors
    {
        White,
        Black,
        Yellow,
        Green,
        Blue,
        Red,
        Orange
    }
    
}
