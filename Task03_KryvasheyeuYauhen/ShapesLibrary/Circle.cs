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
    /// Describes a circle
    /// </summary>
    public abstract class Circle : Shape
    {
        private double _radius;

        public double Radius
        {
            get
            {
                return _radius;
            }
        }

        /// <summary>
        /// Initializes the circle
        /// </summary>
        /// <param name="radius"></param>
        protected Circle(double radius) 
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("The radius should be greater than zero");
            _radius = radius;
        }

        /// <summary>
        /// Initializes the circle cut from another shape
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="shape"></param>
        protected Circle(double radius, Shape shape) : this(radius)
        {
            if (shape.GetArea() <= Math.PI * (radius * radius))
                throw new ArgumentException("the shape to be cut out should be smaller than the shape from which it is cut");
        }

        /// <summary>
        /// initializes the circle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public Circle(XmlReader reader) : base(reader)
        {        }

        /// <summary>
        /// initializes the circle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public Circle(StreamReader reader) : base(reader)
        {        }

        /// <summary>
        /// Calculates the area of the circle
        /// </summary>
        /// <returns></returns>
        public override double GetArea()
        {
            return Math.PI * (Radius * Radius);
        }

        /// <summary>
        /// Calculates the perimeter of the circle
        /// </summary>
        /// <returns></returns>
        public override double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string ToString()
        {
            return string.Format("Circle: Radius = {0}", Radius);
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
        /// initializes the circle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();
            _radius = reader.ReadElementContentAsInt("radius", ""); 
        }

        /// <summary>
        /// Writes the circle to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("radius", Radius.ToString());
        }

        /// <summary>
        /// initializes the circle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(StreamReader reader)
        {    
            _radius = double.Parse(reader.ReadLine().Trim(new char[] { ' ', '<', '>', '/', 'r', 'a', 'd', 'i', 'u','s' }));
        }

        /// <summary>
        /// Writes the circle to Xml file using streamWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(StreamWriter writer)
        {
            writer.Write("    <radius>");
            writer.Write(Radius);
            writer.WriteLine("</radius>");            
        }

    }

    /// <summary>
    /// Describes a circle made of paper
    /// </summary>
    public class PaperCircle : Circle, IPaperShape
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
        /// Initializes the circle made of paper
        /// </summary>
        /// <param name="radius"></param>
        public PaperCircle(double radius) : base(radius)
        { }
        /// <summary>
        /// Initializes the circle cut from another shape
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="shape"></param>
        public PaperCircle(double radius, Shape shape) : base(radius, shape)
        {
            if (shape is IPaperShape)
                _color = ((IPaperShape)shape).Color;
        }

        /// <summary>
        /// initializes the paper circle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public PaperCircle(XmlReader reader) : base(reader)
        {        }


        /// <summary>
        /// initializes the paper circle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public PaperCircle(StreamReader reader) : base(reader)
        {        }

        /// <summary>
        /// Paint the circle
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
            return "[Paper" + base.ToString() + string.Format("; Color = {0}]", Color);
        }

        /// <summary>
        /// initializes the paper circle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            _color = (Colors)Enum.Parse(typeof(Colors), reader.ReadElementContentAsString("color", ""), true);
            reader.ReadEndElement();
        }


        /// <summary>
        /// Writes the paper circle to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("papercircle");
            base.WriteXml(writer);
            writer.WriteElementString("color", Color.ToString());
            writer.WriteEndElement();
        }

        /// <summary>
        /// initializes the paper circle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(StreamReader reader)
        {
            base.ReadXml(reader);
            _color = (Colors)Enum.Parse(typeof(Colors), reader.ReadLine().Trim(new char[] { ' ', '<', '>', '/', 'c', 'o', 'l', 'o', 'r' }),true);
            reader.ReadLine();
        }


        /// <summary>
        /// Writes the paper circle to Xml file using StreamWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(StreamWriter writer)
        {
            writer.WriteLine("  <papercircle>");
            base.WriteXml(writer);
            writer.WriteLine("    <color>{0}</color>", Color);
            writer.WriteLine("  </papercircle>");
        }
    }

    /// <summary>
    /// describes a circle made of film
    /// </summary>
    public class FilmCircle : Circle
    {
        /// <summary>
        /// Initializes the circle cut from another shape
        /// </summary>
        /// <param name="radius"></param>
        public FilmCircle(double radius) : base(radius)
        {
        }

        /// <summary>
        /// Paint the circle
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="shape"></param>
        public FilmCircle(double radius, Shape shape) : base(radius, shape)
        {
        }

        /// <summary>
        /// initializes the film circle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public FilmCircle(XmlReader reader) : base(reader)
        {        }

        /// <summary>
        /// initializes the film circle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public FilmCircle(StreamReader reader) : base(reader)
        {        }

        public override string ToString()
        {
            return "[Film" + base.ToString() + "]";
        }

        /// <summary>
        /// initializes the film circle with data from XmlReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);
            reader.ReadEndElement();
        }


        /// <summary>
        /// Writes the film circle to Xml file using XmlWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("filmcircle");
            base.WriteXml(writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// initializes the film circle with data from StreamReader
        /// </summary>
        /// <param name="reader"></param>
        public override void ReadXml(StreamReader reader)
        {
            base.ReadXml(reader);
            reader.ReadLine();
        }


        /// <summary>
        /// Writes the film circle to Xml file using StreamWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteXml(StreamWriter writer)
        {
            writer.WriteLine("  <filmcircle>");
            base.WriteXml(writer);
            writer.WriteLine("  </filmcircle>");
        }
    }
}
