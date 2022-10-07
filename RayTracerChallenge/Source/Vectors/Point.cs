namespace RT.Source.Vectors
{
    public class Point : Tuple
    {
        public Point(float x, float y, float z) : base(x, y, z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            w = 1;
        }
    }
}
