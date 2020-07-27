using System;


namespace MathObjects
{
    /// <summary>
    /// describes 3D vector
    /// </summary>
    public class Vector
    {
        //coordinates
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
            } 
        }

        public override string ToString()
        {
            return string.Format("[{0}; {1}; {2}]", X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            if(obj is Vector && obj != null)
            {
                Vector temp = (Vector)obj;
                if (temp.X == this.X && temp.Y == this.Y && temp.Z == this.Z)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            hash = hash * 23 + Z.GetHashCode();

            return hash;
        }

        public static bool operator == (Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator != (Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
        }

        /// <summary>
        /// adds two vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator + (Vector v1, Vector v2)
        {
            double x = v1.X + v2.X;
            double y = v1.Y + v2.Y;
            double z = v1.Z + v2.Z;

            return new Vector(x, y, z);
        }

        /// <summary>
        /// subtracts vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator - (Vector v1, Vector v2)
        {
            double x = v1.X - v2.X;
            double y = v1.Y - v2.Y;
            double z = v1.Z - v2.Z;

            return new Vector(x, y, z);
        }

        /// <summary>
        /// multiplies vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator * (Vector v1, Vector v2)
        {
            double x = (v1.Y * v2.Z) - (v1.Z * v2.Y);
            double y = (v1.Z * v2.X) - (v1.X * v2.Z);
            double z = (v1.X * v2.Y) - (v1.Y * v2.X);

            return new Vector(x, y, z);
        }

        /// <summary>
        /// multiplies a vector by a number
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Vector operator * (Vector vector, double number)
        {
            return new Vector(number * vector.X, number * vector.Y, number * vector.Z);
        }

        public static Vector operator * (double number, Vector vector)
        {
            return vector * number;
        }

        /// <summary>
        /// divides a vector by a number
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Vector operator / (Vector vector, double number)
        {
            return vector * (1 / number);
        }
        /// <summary>
        /// Scalarly multiplies vectors
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector ScalarMultiply(Vector v1, Vector v2)
        {
            double x = v1.X * v2.X;
            double y = v1.Y * v2.Y;
            double z = v1.Z * v2.Z;

            return new Vector(x, y, z);
        }

    }
}
