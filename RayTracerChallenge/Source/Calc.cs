namespace RT.Source
{
    public static class Calc
    {
        const float EPSILON = 0.00001f;

        public static bool Equals(float a, float b)
        {
            if (Math.Abs(a - b) < EPSILON) return true;
            return false;
        }
    }
}
