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

        private (Intersection?, Intersection?) IntersectShape(Figure shape)
        {
            Ray invRay = Transform(shape.transform.Inverse());

            // Find discriminant
            Vector shape_to_ray = new(invRay.origin - shape.origin);

            double a = Vector.DotProduct(invRay.direction, invRay.direction);
            double b = 2 * Vector.DotProduct(invRay.direction, shape_to_ray);
            double c = Vector.DotProduct(shape_to_ray, shape_to_ray) - 1;
            double discriminant = b * b - 4 * a * c;

            if (discriminant >= 0)
            {
                double sqrtD = Sqrt(discriminant);
                double t1 = (-b + sqrtD) / (2 * a);
                double t2 = (-b - sqrtD) / (2 * a);

                return (new Intersection(t1, shape), new Intersection(t2, shape));
            }

            return (null, null);
        }

        public Intersections IntersectionsWith(Figure shape) => IntersectionsWith(new[] { shape });

        public Intersections IntersectionsWith(IEnumerable<Figure> shapes)
        {
            Intersections intersections = new();

            foreach (Figure shape in shapes)
            {
                (Intersection?, Intersection?) xs = IntersectShape(shape);
                if (xs.Item1 is not null)
                {
                    intersections.Insert(xs.Item1);
                    intersections.Insert(xs.Item2!);
                }
            }

            return intersections;
        }

        #endregion

        public Ray Transform(Matrix m) => new(new Point(m * origin), new Vector(m * direction));
    }
}
