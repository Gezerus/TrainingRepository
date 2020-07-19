using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MathObjects
{
    /// <summary>
    /// Describes a polynomial
    /// </summary>
    public class Polynomial
    {
        private double[] _coefficients;

        public Polynomial(params double[] coefficients)
        {
            var temp = new double[coefficients.Length];
            Array.Copy(coefficients, temp, coefficients.Length);
            // delete zeros
            for( int i = temp.Length - 1; temp[i] == 0 && i != 0; i--)
                temp = temp.Take(temp.Length-1).ToArray();

            _coefficients = new double[temp.Length];
            Array.Copy(temp, _coefficients, temp.Length);
        }

        public override string ToString()
        {
            var result = new StringBuilder("[");
            for (int i = _coefficients.Length - 1; i >= 0; i--)
            {
                result.Append(string.Format("{0}x^{1}", _coefficients[i], i));
                if (i != 0)
                    result.Append(" + ");
            }

            result.Append("]");

            return result.ToString();
        
        }

        public override bool Equals(object obj)
        {
            if (obj is Polynomial && obj != null)
            {
                Polynomial temp = (Polynomial)obj;
                if (temp._coefficients.Length != this._coefficients.Length)
                    return false;
                for (int i = 0; i < this._coefficients.Length; i++)
                    if (temp._coefficients[i] != this._coefficients[i])
                        return false;
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            int hash = 3;
            foreach (double coef in _coefficients)
                hash = hash * 7 + coef.GetHashCode();
            return hash;

        }

        public static Polynomial operator + (Polynomial p1, Polynomial p2)
        {
            double[] result;

            if(p1._coefficients.Length > p2._coefficients.Length)
            {
                result = new double[p1._coefficients.Length];

                for(int i = 0; i < p1._coefficients.Length; i++)
                {
                    if (i < p2._coefficients.Length)
                        result[i] = p1._coefficients[i] + p2._coefficients[i];
                    else
                        result[i] = p1._coefficients[i];
                }
            }
            else
            {
                result = new double[p2._coefficients.Length];

                for(int i = 0; i < p2._coefficients.Length; i++)
                {
                    if (i < p1._coefficients.Length)
                        result[i] = p1._coefficients[i] + p2._coefficients[i];
                    else result[i] = p2._coefficients[i];
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator - (Polynomial p1, Polynomial p2)
        {
            double[] result;

            if (p1._coefficients.Length > p2._coefficients.Length)
            {
                result = new double[p1._coefficients.Length];

                for (int i = 0; i < p1._coefficients.Length; i++)
                {
                    if (i < p2._coefficients.Length)
                        result[i] = p1._coefficients[i] - p2._coefficients[i];
                    else
                        result[i] = p1._coefficients[i];
                }
            }
            else
            {
                result = new double[p2._coefficients.Length];

                for (int i = 0; i < p2._coefficients.Length; i++)
                {
                    if (i < p1._coefficients.Length)
                        result[i] = p1._coefficients[i] - p2._coefficients[i];
                    else result[i] = -p2._coefficients[i];
                }
            }

            return new Polynomial(result);
        }

        public static Polynomial operator * (Polynomial p1, Polynomial p2)
        {
            var result = new double[p1._coefficients.Length + p2._coefficients.Length - 1];

            for (int i = 0; i < p1._coefficients.Length; i++)
                for (int j = 0; j < p2._coefficients.Length; j++)
                    result[i + j] += p1._coefficients[i] * p2._coefficients[j];
            return new Polynomial(result);
        }

        public static Polynomial operator * (Polynomial polynomial, double number)
        {
            var result = polynomial._coefficients.Select(coefficient => coefficient * number).ToArray();

            return new Polynomial(result);
        }

        public static Polynomial operator * (double number, Polynomial polynomial)
        {
            return polynomial * number;
        }

        /// <summary>
        /// divides polynomials with remainder (Polynomial long division)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="remainder"></param>
        /// <param name="quotient">optional parameter storing coefficients of a quotient from division</param>
        /// <returns>quotient from division</returns>
        public static Polynomial DivisionWithRemainder(Polynomial p1, Polynomial p2, out Polynomial remainder, List<double> quotient = null)
        {

            if (p2._coefficients.Length == 1 && p2._coefficients[0] == 0)
                throw new DivideByZeroException("division by zero");

            if (p2._coefficients.Length > p1._coefficients.Length)
                throw new ArgumentException("the degree of the divisor cannot be greater than the degree of divisible");
            //if the iteration is first, initialize list storing coefficients of a quotient from division
            if (quotient == null)
            quotient = new List<double>();
            // divide the first term of the dividend by the first term of the divisor
            quotient.Add(p1._coefficients[p1._coefficients.Length - 1] / p2._coefficients[p2._coefficients.Length - 1]);
            // calculate polynomial degree 
            int degree = (p1._coefficients.Length - 1) - (p2._coefficients.Length - 1);
            // initialize polynomial and multiply its by p2
            var tempCoefficients = new double[degree + 1];
            tempCoefficients[tempCoefficients.Length - 1] = quotient[quotient.Count - 1];
            var temp = new Polynomial(tempCoefficients) * p2;
            
            remainder = p1 - temp;
            // recursion exit condition
            if (remainder == new Polynomial(0) || remainder._coefficients.Length < p2._coefficients.Length)
            {
                quotient.Reverse();
                return new Polynomial(quotient.ToArray());
            }

            return DivisionWithRemainder(remainder, p2, out remainder, quotient);
        }
    }
}
