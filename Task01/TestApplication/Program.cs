using ShapesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\Files\ShapesFiile.txt";

            var shapes = ShapeLoader.Load(path);

            var circle = new Circle(30);

            var filteredShapes = shapes.Where(shape => shape.Equals(circle));

            foreach (Shape s in filteredShapes)
                Console.WriteLine(s.ToString());

            Console.ReadLine();
        }
    }
}
