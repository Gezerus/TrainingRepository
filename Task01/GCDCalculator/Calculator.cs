using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDCalculator
{
    /// <summary>
    /// class calculating GCD 
    /// </summary>
    static public class Calculator
    {
        /// <summary>
        /// Calculation of GCD of two integer numbers using Euclidean algoritm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="methodTime">runtime of this method</param>
        /// <returns>GCD</returns>
        static public int GCD(int a, int b, out TimeSpan methodTime)
        {

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            //a must always be greater than b 
            if (Math.Abs(a) < Math.Abs(b))
            {
                int temp = a;
                a = b;
                b = temp;
            }
            if (b == 0)
            {
                //go back
                stopWatch.Stop();
                methodTime = stopWatch.Elapsed;
                return Math.Abs(a);
            }

            int result = GCD(b, a % b, out TimeSpan time);

            stopWatch.Stop();
            methodTime = stopWatch.Elapsed;

            return result;
        }
        /// <summary>
        /// Calculation of GCD of three integer numbers using Euclidean algoritm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>GCD</returns>
        static public int GCD(int a, int b, int c)
        {
            TimeSpan methodTime;

            if (a == 0)
                return GCD(b, c, out methodTime);
            if (b == 0)
                return GCD(a, c, out methodTime);
            if (c == 0)
                return GCD(a, b, out methodTime);

            int[] array = { Math.Abs(a), Math.Abs(b), Math.Abs(c) };

            Array.Sort(array);

            a = array[2]; 
            b = array[1]; 
            c = array[0];

            return GCD(c, a % c, b % c);
        }
        /// <summary>
        /// Calculation of GCD of four integer numbers using Euclidean algoritm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns>GCD</returns>
        static public int GCD(int a, int b, int c, int d)
        {
            if (a == 0)
                return GCD(b, c, d);
            if (b == 0)
                return GCD(a, c, d);
            if (c == 0)
                return GCD(a, b, d);
            if (d == 0)
                return GCD(a, b, c);

            int[] array = { Math.Abs(a), Math.Abs(b), Math.Abs(c), Math.Abs(d) };

            Array.Sort(array);

            a = array[3]; 
            b = array[2]; 
            c = array[1]; 
            d = array[0];

            return GCD(d, a % d, b % d, c % d);
        }
        /// <summary>
        /// Calculation of GCD of five integer numbers using Euclidean algoritm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns>GCD</returns>
        static public int GCD(int a, int b, int c, int d, int e)
        {
            if (a == 0)
                return GCD(b, c, d, e);
            if (b == 0)
                return GCD(a, c, d, e);
            if (c == 0)
                return GCD(a, b, d, e);
            if (d == 0)
                return GCD(a, b, c, e);
            if (e == 0)
                return GCD(a, b, c, d);

            int[] array = { Math.Abs(a), Math.Abs(b), Math.Abs(c), Math.Abs(d), Math.Abs(e) };

            Array.Sort(array);

            a = array[4]; 
            b = array[3]; 
            c = array[2]; 
            d = array[1]; 
            e = array[0];

            return GCD(e, a % e, b % e, c % e, d % e);
        }

        /// <summary>
        /// Calculation of GCD of two integer numbers using Stein's algorithm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="methodTime">untime of this method</param>
        /// <returns>GCD</returns>
        static public int BinaryGCD(int a, int b, out TimeSpan methodTime)
        {

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == b)
            {
                stopWatch.Stop();
                methodTime = stopWatch.Elapsed;

                return a;
            }
            if (a == 0)
            {
                stopWatch.Stop();
                methodTime = stopWatch.Elapsed;

                return b;
            }
            if (b == 0)
            {
                stopWatch.Stop();
                methodTime = stopWatch.Elapsed;

                return a;
            }
            int result;
            if (a % 2 == 0)
                if (b % 2 == 0)
                {
                    result = 2 * BinaryGCD(a / 2, b / 2, out TimeSpan time);

                    stopWatch.Stop();
                    methodTime = stopWatch.Elapsed;

                    return result;
                }
                else
                {
                    result = BinaryGCD(a / 2, b, out TimeSpan time);

                    stopWatch.Stop();
                    methodTime = stopWatch.Elapsed;

                    return result;
                }
            if (b % 2 == 0)
            {
                result = BinaryGCD(a, b / 2, out TimeSpan time);

                stopWatch.Stop();
                methodTime = stopWatch.Elapsed;

                return result;
            }
            if (a > b)
            {
                result = BinaryGCD((a - b) / 2, b, out TimeSpan time);

                stopWatch.Stop();
                methodTime = stopWatch.Elapsed;

                return result;
            }
            result = BinaryGCD((b - a) / 2, a, out TimeSpan mTime);

            stopWatch.Stop();
            methodTime = stopWatch.Elapsed;

            return result;
        }

        /// <summary>
        /// Prepares data for a histogram
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>runtimes GCD and BinaryGCD</returns>
        /// <exception cref="System.IndexOutOfRangeException">Thrown when more or less than two values ​​are passed to the method to calculate the GCD</exception>
        static public List<TimeSpan[]> PrepareHistogramData(int[][] numbers)
        {            
            var timeIntervals = new List<TimeSpan[]>();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].Length != 2)
                    throw new IndexOutOfRangeException("The array must contains only two values: runtimes of GCD and BinaryGCD");

                GCD(numbers[i][0], numbers[i][1], out TimeSpan gcdInterval);
                BinaryGCD(numbers[i][0], numbers[i][1], out TimeSpan binaryGcdInterval);
                timeIntervals.Add(new TimeSpan[] { gcdInterval, binaryGcdInterval });
            }
            return timeIntervals;
        }

    }
}