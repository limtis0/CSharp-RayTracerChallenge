using RT.Source.Matrices;

namespace RT.Source.Vectors
{
    public class Tuple
    {
        public double x;
        public double y;
        public double z;
        public double w;

        public Tuple()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        // For Point/Vector inheritance
        public Tuple(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Tuple(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public bool IsPoint() => w == 1;

        public bool IsVector() => w == 0;

        #region Operators

        public static Tuple operator -(Tuple a) => new(-a.x, -a.y, -a.z, -a.w);

        public static Tuple operator +(Tuple a, Tuple b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

        public static Tuple operator -(Tuple a, Tuple b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);

        public static Tuple operator *(Tuple a, double b) => new(a.x * b, a.y * b, a.z * b, a.w * b);

        public static Tuple operator /(Tuple a, double b) => new(a.x / b, a.y / b, a.z / b, a.w / b);

        public static bool operator ==(Tuple a, Tuple b)
        {
            return Calc.Equals(a.x, b.x) && Calc.Equals(a.y, b.y) && Calc.Equals(a.z, b.z) && Calc.Equals(a.w, b.w);
        }

        public static bool operator !=(Tuple a, Tuple b)
        {
            return !Calc.Equals(a.x, b.x) || !Calc.Equals(a.y, b.y) || !Calc.Equals(a.z, b.z) || !Calc.Equals(a.w, b.w);
        }

        public override bool Equals(object? obj) => obj is Tuple item && this == item;

        public override int GetHashCode() => HashCode.Combine(x, y, z, w);

        public override string ToString() => $"Tuple(x:{x}, y:{y}, z:{z}, w:{w})";

        #endregion

        #region Transformations

        public Tuple Translate(double x, double y, double z, bool inverse = false) => Matrix.Translation(x, y, z, inverse) * this;

        public Tuple Scale(double x, double y, double z, bool inverse = false) => Matrix.Scaling(x, y, z, inverse) * this;

        public Tuple RotateX(double rad, bool inverse = false) => Matrix.RotationX(rad, inverse) * this;

        public Tuple RotateY(double rad, bool inverse = false) => Matrix.RotationY(rad, inverse) * this;

        public Tuple RotateZ(double rad, bool inverse = false) => Matrix.RotationZ(rad, inverse) * this;

        public Tuple Skew(double xy, double xz, double yx, double yz, double zx, double zy, bool inverse = false)
            => Matrix.Skewing(xy, xz, yx, yz, zx, zy, inverse) * this;

        #endregion
    }
}
