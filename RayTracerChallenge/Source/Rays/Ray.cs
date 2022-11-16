using RT.Source.Figures;
using RT.Source.Matrices;
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

        public Intersections IntersectionsWith(Figure shape)
        {
            Ray invRay = Transform(shape.transform.Inverse());

            // Find discriminant
            Vector shape_to_ray = (invRay.origin - shape.origin).ToVector();

            float a = Vector.DotProduct(invRay.direction, invRay.direction);
            float b = 2 * Vector.DotProduct(invRay.direction, shape_to_ray);
            float c = Vector.DotProduct(shape_to_ray, shape_to_ray) - 1;
            float discriminant = b * b - 4 * a * c;

            // Find intersections
            Intersections intersections = new();

            if (discriminant >= 0)
            {
                float sqrtD = (float)Math.Sqrt(discriminant);
                float t1 = (-b + sqrtD) / (2 * a);
                float t2 = (-b - sqrtD) / (2 * a);

                intersections.Insert(new Intersection(t1, shape));
                intersections.Insert(new Intersection(t2, shape));
            }

            return intersections;
        }

        public Ray Transform(Matrix m) => new((m * origin).ToPoint(), (m * direction).ToVector());
    }
}
