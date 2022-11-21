namespace RT.Source.Vectors
{
    public class Vector : Tuple
    {
        public Vector(float x, float y, float z) : base(x, y, z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            w = 0;
        }

        public Vector(Tuple t)
        {
            if (t.IsVector())
            {
                x = t.x;
                y = t.y;
                z = t.z;
                w = 0;
            }
            else throw new ArgumentException($"Can not cast {this} to a Vector: w != 0");
        }

        // Basically vector distance
        public static float Magnitude(Vector a) => (float)System.Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);

        // Returns vector with magnitude == 1
        public static Vector Normalize(Vector a)
        {
            float magn = Magnitude(a);
            return new Vector(a.x / magn, a.y / magn, a.z / magn);
        }

        // Has values between -1 to 1; Lower the value - bigger the angle between vectors
        public static float DotProduct(Vector a, Vector b) => a.x * b.x + a.y * b.y + a.z * b.z;

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
