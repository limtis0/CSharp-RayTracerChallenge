namespace RT.Source.Vectors
{
    public class Tuple
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Tuple()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

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

        #region Operators

        public static Tuple operator -(Tuple a) { return new Tuple(-a.x, -a.y, -a.z, -a.w); }

        public static Tuple operator +(Tuple a, Tuple b) { return new Tuple(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); }

        public static Tuple operator -(Tuple a, Tuple b) { return new Tuple(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w); }

        public static Tuple operator *(Tuple a, float b) { return new Tuple(a.x * b, a.y * b, a.z * b, a.w * b); }

        public static Tuple operator /(Tuple a, float b) { return new Tuple(a.x / b, a.y / b, a.z / b, a.w / b); }

        public static bool operator ==(Tuple a, Tuple b)
        {
            return Calc.Equals(a.x, b.x) && Calc.Equals(a.y, b.y) && Calc.Equals(a.z, b.z) && Calc.Equals(a.w, b.w);
        }

        public static bool operator !=(Tuple a, Tuple b)
        {
            return !Calc.Equals(a.x, b.x) || !Calc.Equals(a.y, b.y) || !Calc.Equals(a.z, b.z) || !Calc.Equals(a.w, b.w);
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
}
