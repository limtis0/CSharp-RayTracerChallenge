using RT.Source.Vectors;

namespace RT.Source.Rays
{
    public class Ray
    {
        public Point origin;
        public Vector direction;

        public Ray(Point origin, Vector direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public Point Position(float t) => (origin + direction * t).ToPoint();
    }
}
