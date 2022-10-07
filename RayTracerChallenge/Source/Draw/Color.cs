namespace RT.Source.Draw
{
    public class Color
    {
        public float r;
        public float g;
        public float b;

        public Color()
        {
            r = 0;
            g = 0;
            b = 0;
        }

        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        #region Operators

        public static Color operator +(Color a, Color b) { return new Color(a.r + b.r, a.g + b.g, a.b + b.b); }

        public static Color operator -(Color a, Color b) { return new Color(a.r - b.r, a.g - b.g, a.b - b.b); }

        public static Color operator *(Color a, Color b) { return new Color(a.r * b.r, a.g * b.g, a.b * b.b); }

        public static Color operator *(Color a, float b) { return new Color(a.r * b, a.g * b, a.b * b); }

        public static bool operator ==(Color a, Color b) { return Calc.Equals(a.r, b.r) && Calc.Equals(a.g, b.g) && Calc.Equals(a.b, b.b); }

        public static bool operator !=(Color a, Color b) { return !Calc.Equals(a.r, b.r) || !Calc.Equals(a.g, b.g) || !Calc.Equals(a.b, b.b); }

        public override bool Equals(object? obj)
        {
            if (obj is not Color item)
            {
                return false;
            }

            return this == item;
        }

        public override int GetHashCode() { return HashCode.Combine(r, g, b); }

        // Used for PPM writing
        public override string ToString() { return $"{To255Format(r)} {To255Format(g)} {To255Format(b)}"; }

        private static int To255Format(float colorValue) { return Math.Clamp((int)(colorValue * 255), 0, 255); }

        #endregion
    }
}
