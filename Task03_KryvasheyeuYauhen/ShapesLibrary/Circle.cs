using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return "[Film" + base.ToString() + "]";
        }
    }
}
