using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesLibrary
{
    /// <summary>
    /// Describes a equilateral triangle
    /// </summary>
    public abstract class EquilateralTriangle : Shape
    {
        private double _side;

        public double Side
        {
            get
            {
                return _side;
            }
        }

        public double Height
        {
            get
            {
                return Math.Sqrt(Side * Side - Side * Side / 4);
            }
        }

        /// <summary>
        /// Initializes the triangle
        /// </summary>
        /// <param name="side"></param>
        protected EquilateralTriangle(double side)
        {
            if (side <= 0)
                throw new ArgumentOutOfRangeException("The side should be greater than zero");
            _side = side;
        }

        /// <summary>
        /// Initializes the triangle cut from another shape
        /// </summary>
        /// <param name="side"></param>
        /// <param name="shape"></param>
        protected EquilateralTriangle(double side, Shape shape) : this(side)
        {
            if (shape.GetArea() <= side * Math.Sqrt(side * side - side * side / 4) / 2)
                throw new ArgumentException("the shape to be cut out should be smaller than the shape from which it is cut");
        }

        /// <summary>
        /// Calculates the area of ​​the triangle
        /// </summary>
        /// <returns></returns>
        public override double GetArea()
        {
            return Height * Side / 2;
        }

        /// <summary>
        /// Calculateы the perimeter of the triangle
        /// </summary>
        /// <returns></returns>
        public override double GetPerimeter()
        {
            return 3 * Side;
        }

        public override string ToString()
        {
            return string.Format("EquilateralTriangle: Side = {0}", Side);
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
    /// Describes a triangle made of paper
    /// </summary>
    public class PaperEquilateralTriangle : EquilateralTriangle, IPaperShape
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
        /// Initializes the triangle made of paper
        /// </summary>
        /// <param name="side"></param>
        public PaperEquilateralTriangle(double side) : base(side)
        { }

        /// <summary>
        /// Initializes the triangle cut from another shape
        /// </summary>
        /// <param name="side"></param>
        /// <param name="shape"></param>
        public PaperEquilateralTriangle(double side, Shape shape) : base(side, shape)
        {
            if (shape is IPaperShape)
                _color = ((IPaperShape)shape).Color;
        }

        /// <summary>
        /// Paint the triangle
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
    }

    /// <summary>
    /// Describes a triangle made of film
    /// </summary>
    public class FilmEquilateralTriangle : EquilateralTriangle
    {
        /// <summary>
        /// Initializes the triangle made of film
        /// </summary>
        /// <param name="side"></param>
        public FilmEquilateralTriangle(double side) : base(side)
        {        }

        /// <summary>
        /// Initializes the triangle cut from another shape
        /// </summary>
        /// <param name="side"></param>
        /// <param name="shape"></param>
        public FilmEquilateralTriangle(double side, Shape shape) : base(side, shape)
        {        }

        public override string ToString()
        {
            return "[Film" + base.ToString() + "]";
        }
    }
}
