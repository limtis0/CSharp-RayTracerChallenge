namespace RT.Source
{
    public static class Calc
    {
        public const double Epsilon = 0.00001;

        public static bool Equals(double a, double b) => Math.Abs(a - b) < Epsilon;
    }
}
