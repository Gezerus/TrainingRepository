using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesLibrary
{
    /// <summary>
    /// abstract class for all shapes
    /// </summary>
    public abstract class Shape
    {
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
