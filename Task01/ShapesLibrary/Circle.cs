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
    public class Circle : Shape
    {
        private double _radius;

        /// <summary>
        /// The radius should be greater than 0 
        /// </summary>
        public double Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("The radius should be greater than zero");
                _radius = value;

            }
        }

        /// <summary>
        /// initializes a circle
        /// </summary>
        /// <param name="radius"></param>
        public Circle(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Calculates the area of ​​the circle
        /// </summary>
        /// <returns>The circle area</returns>
        public override double GetArea()
        {
            return Math.PI * (Radius * Radius);
        }

        /// <summary>
        /// Calculates the perimeter of the circle
        /// </summary>
        /// <returns>The circle perimeter</returns>
        public override double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string ToString()
        {
            return string.Format("[Circle: Radius = {0}]", Radius);
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
}
