using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public override string ToString()
        {
            return "[Film" + base.ToString() + "]";
        }
    }

}
