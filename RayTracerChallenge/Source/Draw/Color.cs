namespace RT.Source.Draw
{
    public class Color
    {
        public double r;
        public double g;
        public double b;

        public Color()
        {
            r = 0;
            g = 0;
            b = 0;
        }

        public Color(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        #region Operators

        public static Color operator +(Color a, Color b) => new(a.r + b.r, a.g + b.g, a.b + b.b);

        public static Color operator -(Color a, Color b) => new(a.r - b.r, a.g - b.g, a.b - b.b);

        public static Color operator *(Color a, Color b) => new(a.r * b.r, a.g * b.g, a.b * b.b);

        public static Color operator *(Color a, double b) => new(a.r * b, a.g * b, a.b * b);

        public static bool operator ==(Color a, Color b) => Calc.Equals(a.r, b.r) && Calc.Equals(a.g, b.g) && Calc.Equals(a.b, b.b);

        public static bool operator !=(Color a, Color b) => !Calc.Equals(a.r, b.r) || !Calc.Equals(a.g, b.g) || !Calc.Equals(a.b, b.b);

        public override bool Equals(object? obj)
        {
            if (obj is not Color item)
            {
                return false;
            }

            return this == item;
        }

        public override int GetHashCode() => HashCode.Combine(r, g, b);

        // Used for PPM writing
        public string ToRGB() => $"{To255Format(r)} {To255Format(g)} {To255Format(b)}";

        private static int To255Format(double colorValue) => Math.Clamp((int)(colorValue * 255), 0, 255);

        public override string ToString() => $"Color: ({ToRGB()})";

        #endregion
    }
}
