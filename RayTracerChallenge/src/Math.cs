namespace RT
{
    public static class Math
    {
        const float EPSILON = 0.00001f;

        public static bool Equals(float a, float b)
        {
            if (System.Math.Abs(a - b) < EPSILON) return true;
            return false;
        }
    }
}
