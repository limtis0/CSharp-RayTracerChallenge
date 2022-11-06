namespace RT.Source
{
    public static class Calc
    {
        public const float PI = (float) Math.PI;
        private const float Epsilon = 0.00001f;

        public static bool Equals(float a, float b) => Math.Abs(a - b) < Epsilon;
    }
}
