namespace RT.Source
{
    public static class Calc
    {
        const float Epsilon = 0.00001f;

        public static bool Equals(float a, float b)
        {
            if (Math.Abs(a - b) < Epsilon) return true;
            return false;
        }
    }
}
