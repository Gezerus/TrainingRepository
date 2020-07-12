using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapesLibrary
{
    public static class ShapeLoader
    {
        /// <summary>
        /// Loads shapes from a text file into an array
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>array of shapes</returns>
        public static Shape [] Load(string path)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(path); 
            }
            catch(Exception e)
            {
                throw e;
            }
            if (lines.Length == 0)
                throw new Exception("File is empty");

            var result = new Shape[lines.Length];
            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("Rectangle"))
                        result[i] = ConvertToRectangle(lines[i]);
                    else if (lines[i].Contains("Triangle"))
                        result[i] = ConvertToTriangle(lines[i]);
                    else if (lines[i].Contains("Circle"))
                        result[i] = ConvertToCircle(lines[i]);
                    else
                        throw new Exception("Incorect data");
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return result;           
        }

        /// <summary>
        /// Convert from string to rectancle
        /// </summary>
        /// <param name="line">a line from the file</param>
        /// <returns>A rectangle</returns>
        private static Rectangle ConvertToRectangle(string line)
        {
            double height;
            double width;

            string[] splitedLine = line.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            if (splitedLine.Length != 3)
                throw new Exception("Incorect data");

            try
            {                
                height = double.Parse(splitedLine[1]);
                width = double.Parse(splitedLine[2]);
            }
            catch(Exception e)
            {
                throw e;
            }

            return new Rectangle(height, width);
        }

        /// <summary>
        /// Convert from string to triangle
        /// </summary>
        /// <param name="line">a line from the file</param>
        /// <returns>A triangle</returns>
        private static Triangle ConvertToTriangle(string line)
        {
            double a;
            double b;
            double c;

            string[] splitedLine = line.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            if (splitedLine.Length != 4)
                throw new Exception("Incorect data");

            try
            {                
                a = double.Parse(splitedLine[1]);
                b = double.Parse(splitedLine[2]);
                c = double.Parse(splitedLine[3]);
            }
            catch (Exception e)
            {
                throw e;
            }

            return new Triangle(a, b, c);
        }

        /// <summary>
        /// Convert from string to circle
        /// </summary>
        /// <param name="line">a line from the file</param>
        /// <returns>A triangle</returns>
        private static Circle ConvertToCircle(string line)
        {
            double radius;

            string[] splitedLine = line.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            if (splitedLine.Length != 2)
                throw new Exception("Incorect data");

            try
            {                
                radius = double.Parse(splitedLine[1]);
            }
            catch (Exception e)
            {
                throw e;
            }

            return new Circle(radius);
        }
    }


}
