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
    public class Rectangle : Shape 
    {  
        private double _height;

        private double _width;

        /// <summary>
        /// The height should be greater than 0 
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("The height should be greater than zero");
                _height = value;                
               
            }
        }
        /// <summary>
        /// The width should be greater than 0 
        /// </summary>
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("The width should be greater than zero");
                _width = value;                
            }
        }
        /// <summary>
        /// initializes a rectangle
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Calculates the area of ​​the rectangle
        /// </summary>
        /// <returns>The rectangle area</returns>
        public override double GetArea()
        {
            return this.Height * this.Width;
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
            return string.Format("[Rectangle: Height = {0}; Width = {1}]", Height, Width);
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
