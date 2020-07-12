using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathObjects
{
    public class Polynomial
    {
        private double[] _coefficients;

        public Polynomial(params double[] coefficients)
        {
            _coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, _coefficients, coefficients.Length);
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
            return obj.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
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
    }
}
