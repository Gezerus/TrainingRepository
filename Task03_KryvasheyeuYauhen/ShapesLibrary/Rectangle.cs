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
    /// Describes a rectangle
    /// </summary>
    public abstract class Rectangle : Shape
    {
        private double _height;

        private double _width;
        
        public double Height
        {
            get
            {
                return _height;
            }
        }
        
        public double Width
        {
            get
            {
                return _width;
            }
        }
        /// <summary>
        /// Initializes the rectangle
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        protected Rectangle(double height, double width)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("The width should be greater than zero");
            _width = width;

            if (height <= 0)
                throw new ArgumentOutOfRangeException("The height should be greater than zero");
            _height = height;

        }

        /// <summary>
        /// Initializes the rectangle cut from another shape
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        protected Rectangle(double height, double width, Shape shape) : this(height, width)
        {
            if (shape.GetArea() <= height * width)
                throw new ArgumentException("the shape to be cut out should be smaller than the shape from which it is cut");
        }

        /// <summary>
        /// initializes the rectangle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public Rectangle(XmlReader reader) : base(reader)
        {
        }

        /// <summary>
        /// initializes the rectangle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public Rectangle(StreamReader reader) : base(reader)
        {        }

        /// <summary>
        /// Calculates the area of ​​the rectangle
        /// </summary>
        /// <returns>The rectangle area</returns>
        public override double GetArea()
        {
            return Height * Width;
        }

        /// <summary>
        /// Calculateы the perimeter of the rectangle
        /// </summary>
        /// <returns>the rectangle perimeter</returns>
        public override double GetPerimeter()
        {
            return 2 * Height + 2 * Width;
        }

        public override string ToString()
        {
            return string.Format("Rectangle: Height = {0}; Width = {1}", Height, Width);
        }
        public override bool Equals(object obj)
        {
            return obj.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// initializes the rectangle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            _height= reader.ReadElementContentAsInt("height", "");
            _width = reader.ReadElementContentAsInt("width", "");
        }

        /// <summary>
        /// Writes the rectangle to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("height", Height.ToString());
            writer.WriteElementString("width", Width.ToString());
        }

        /// <summary>
        /// initializes the rectangle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(StreamReader reader)
        {
            _height = double.Parse(reader.ReadLine().Trim(new char[] { ' ', '<', '>', '/', 'h', 'e', 'i', 'g', 'h', 't' }));
            _width = double.Parse(reader.ReadLine().Trim(new char[] { ' ', '<', '>', '/', 'w', 'i', 'd', 't', 'h' }));
        }

        /// <summary>
        /// Writes the circle to Xml file using streamWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(StreamWriter writer)
        {
            writer.WriteLine("    <height>{0}</height>", Height);
            writer.WriteLine("    <width>{0}</width>", Width);
        }
    }

    /// <summary>
    /// Describes a rectangle made of paper
    /// </summary>
    public class PaperRectangle : Rectangle, IPaperShape
    {
        private Colors _color;
        public Colors Color
        {
            get
            {
                return _color;
            }
        }
        /// <summary>
        /// Initializes the rectangle made of paper
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public PaperRectangle(double height, double width) : base(height, width)
        { }

        /// <summary>
        /// Initializes the rectangle cut from another shape
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="shape"></param>
        public PaperRectangle(double height, double width, Shape shape) : base(height, width, shape)
        {
            if (shape is IPaperShape)
                _color = ((IPaperShape)shape).Color;
        }

        /// <summary>
        /// initializes the paper rectngle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public PaperRectangle(XmlReader reader) : base(reader)
        {        }

        /// <summary>
        /// initializes the paper rectngle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public PaperRectangle(StreamReader reader) : base(reader)
        {        }

        /// <summary>
        /// Paint the rectangle
        /// </summary>
        /// <param name="color"></param>
        public void Paint(Colors color)
        {
            if (Color != Colors.White)
                throw new Exception("The shape hasalready painted");
            if (color == Colors.White)
                throw new ArgumentException("the shape cannot be painted white");
            _color = color;
        }

        public override string ToString()
        {
            return "[Paper" + base.ToString() + string.Format("; Color = {0}]",Color);
        }

        /// <summary>
        /// initializes the paper rectangle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);            
            _color = (Colors)Enum.Parse(typeof(Colors), reader.ReadElementContentAsString("color", ""), true);
            reader.ReadEndElement();
        }


        /// <summary>
        /// Writes the paper rectangle to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("paperrectangle");
            base.WriteXml(writer);
            writer.WriteElementString("color", Color.ToString());
            writer.WriteEndElement();
        }

        /// <summary>
        /// initializes the paper rectangle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(StreamReader reader)
        {
            base.ReadXml(reader);            
            _color = (Colors)Enum.Parse(typeof(Colors), reader.ReadLine().Trim(new char[] { ' ', '<', '>', '/', 'c', 'o', 'l', 'o', 'r' }), true);
            reader.ReadLine();
        }


        /// <summary>
        /// Writes the paper rectangle to Xml file using StreamWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(StreamWriter writer)
        {
            writer.WriteLine("  <paperrectangle>");
            base.WriteXml(writer);
            writer.WriteLine("    <color>{0}</color>", Color);
            writer.WriteLine("  </paperrectangle>");
        }
    }

    /// <summary>
    /// Describes a rectangle made of film
    /// </summary>
    public class FilmRectangle : Rectangle
    {
        /// <summary>
        /// Initializes the rectangle made of film
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public FilmRectangle(double height, double width) : base(height, width)
        {        }

        /// <summary>
        /// Initializes the rectangle cut from another shape
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="shape"></param>
        public FilmRectangle(double height, double width, Shape shape) : base(height, width, shape)
        {        }

        /// <summary>
        /// initializes the film rectangle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public FilmRectangle(XmlReader reader) : base(reader)
        {        }

        /// <summary>
        /// initializes the film rectangle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public FilmRectangle(StreamReader reader) : base(reader)
        {        }

        public override string ToString()
        {
            return "[Film" + base.ToString() + "]";
        }

        /// <summary>
        /// initializes the film rectangle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            reader.ReadEndElement();
        }

        /// <summary>
        /// Writes the film rectangle to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("filmrectangle");
            base.WriteXml(writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// initializes the film rectanglewith data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(StreamReader reader)
        {
            base.ReadXml(reader);
            reader.ReadLine();
        }


        /// <summary>
        /// Writes the film rectangle to Xml file using StreamWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(StreamWriter writer)
        {
            writer.WriteLine("  <filmrectangle>");
            base.WriteXml(writer);
            writer.WriteLine("  </filmrectangle>");
        }
    }

}
