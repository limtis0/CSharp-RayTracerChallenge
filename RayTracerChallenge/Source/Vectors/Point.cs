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

        public Point(Tuple t)
        {
            if (t.IsPoint())
            {
                x = t.x;
                y = t.y;
                z = t.z;
                w = 1;
            }
            else throw new ArgumentException($"Can not cast {this} to a Point: w != 1");
        }
    }
}
