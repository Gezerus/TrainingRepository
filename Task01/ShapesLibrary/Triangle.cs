using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesLibrary
{
    /// <summary>
    /// Describes a triangle
    /// </summary>
    public class Triangle : Shape
    {
        private double _a;

        private double _b;

        private double _c;

        public double A
        {
            get
            {
                return _a;
            }

        }

        public double B
        {
            get
            {
                return _b;
            }

        }

        public double C
        {
            get
            {
                return _c;
            }

        }

        /// <summary>
        /// initializes a triangle
        /// </summary>
        /// <remarks>sum of any two sides must be greatest than the third side</remarks>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Triangle(double a, double b, double c)
        {
            if(a <= 0)
                throw new ArgumentOutOfRangeException("The side should be greater than zero");

            if (b <= 0)
                throw new ArgumentOutOfRangeException("The side should be greater than zero");

            if (c <= 0)
                throw new ArgumentOutOfRangeException("The side should be greater than zero");

            if ((a + b > c) && (a + c > b) && (c + b > a))
            {
                _a = a;
                _b = b;
                _c = c;
            }
            else throw new ArgumentOutOfRangeException("The triangle does not exist");
        }

        /// <summary>
        /// Calculates the area of ​​the triangle
        /// </summary>
        /// <returns>The triangle area</returns>
        public override double GetArea()
        {
            double halfPerimeter = GetPerimeter() / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - A) * (halfPerimeter - B) * (halfPerimeter - C));
        }

        /// <summary>
        /// Calculateы the perimeter of the triangle
        /// </summary>
        /// <returns>The triangle perimiter</returns>
        public override double GetPerimeter()
        {
            return A + B + C;
        }

        public override string ToString()
        {
            return string.Format("[Triangle: A = {0}; B = {1}; C = {2}]", A, B, C);
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
