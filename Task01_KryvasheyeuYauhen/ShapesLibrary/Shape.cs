

namespace ShapesLibrary
{   
    /// <summary>
    /// abstract class for all figures
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Calculateы the area of ​​the figure
        /// </summary>
        /// <returns>The figure area</returns>
        public abstract double GetArea();

        /// <summary>
        /// Calculateы the perimeter of the figure
        /// </summary>
        /// <returns>The figure Perimeter</returns>
        public abstract double GetPerimeter();
    }
}
