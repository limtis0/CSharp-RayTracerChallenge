namespace RT.Vectors
{
    public class Tuple
    {
        public float x;
        public float y;
        public float z;
        public float w;

        // For Point/Vector inheritance
        public Tuple(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Tuple(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public bool IsPoint() { return w == 1; }

        public bool IsVector() { return w == 0; }

        #region Operator overrides

        public static Tuple operator -(Tuple a) { return new Tuple(-a.x, -a.y, -a.z, -a.w); }

        public static Tuple operator +(Tuple a, Tuple b) { return new Tuple(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); }

        public static Tuple operator -(Tuple a, Tuple b) { return new Tuple(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w); }

        public static Tuple operator *(Tuple a, float b) { return new Tuple(a.x * b, a.y * b, a.z * b, a.w * b); }

        public static Tuple operator /(Tuple a, float b) { return new Tuple(a.x / b, a.y / b, a.z / b, a.w / b); }

        public static bool operator ==(Tuple a, Tuple b)
        {
            return Math.Equals(a.x, b.x) && Math.Equals(a.y, b.y) && Math.Equals(a.z, b.z) && Math.Equals(a.w, b.w);
        }

        public static bool operator !=(Tuple a, Tuple b)
        {
            return !Math.Equals(a.x, b.x) || !Math.Equals(a.y, b.y) || !Math.Equals(a.z, b.z) || !Math.Equals(a.w, b.w);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Tuple item)
            {
                return false;
            }

            return this == item;
        }

        public override int GetHashCode() { return HashCode.Combine(x, y, z, w); }

        public override string ToString() { return $"Tuple(x:{x}, y:{y}, z:{z}, w:{w})"; }

        #endregion
    }

    public class Point : Tuple
    {
        public Point(float x, float y, float z): base(x, y, z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            w = 1;
        }
    }

    public class Vector : Tuple
    {
        public Vector(float x, float y, float z): base(x, y, z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            w = 0;
        }

        // Basically vector distance
        public static float Magnitude(Vector a)
        {
            return (float)System.Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

        // Returns vector with magnitude == 1
        public static Vector Normalize(Vector a)
        {
            float magn = Magnitude(a);
            return new Vector(a.x / magn, a.y / magn, a.z / magn);
        }

        // Has values between -1 to 1; Lower the value - bigger the angle between vectors
        public static float DotProduct(Vector a, Vector b) { return a.x * b.x + a.y * b.y + a.z * b.z; }

        // Returns a vector, perpendicular to both A and B vectors; Order matters!
        public static Vector CrossProduct(Vector a, Vector b)
        {
            return new Vector(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
                );
        }
    }
}
