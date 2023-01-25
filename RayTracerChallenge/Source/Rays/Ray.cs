using RT.Source.Figures;
using RT.Source.Matrices;
using RT.Source.Vectors;
using static System.Math;

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

        public Point Position(double t) => new(origin + direction * t);

        #region Intersections

        public Intersections IntersectionsWith(Figure shape) => IntersectionsWith(new[] { shape });

        public Intersections IntersectionsWith(IEnumerable<Figure> shapes)
        {
            Intersections intersections = new();

            foreach (Figure shape in shapes)
                foreach (Intersection intersection in shape.Intersect(this))
                    intersections.Insert(intersection);

            return intersections;
        }

        #endregion

        public Ray Transform(Matrix m) => new(new Point(m * origin), new Vector(m * direction));
    }
}
