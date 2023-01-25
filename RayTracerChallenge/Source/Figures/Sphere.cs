using RT.Source.Materials;
using RT.Source.Matrices;
using RT.Source.Rays;
using RT.Source.Vectors;
using static System.Math;

namespace RT.Source.Figures
{
    public class Sphere : Figure
    {
        public const double Radius = 1;

        protected override Vector LocalNormalAt(Point point) => new(point - origin);

        protected override IEnumerable<Intersection> LocalIntersect(Ray localRay)
        {
            Vector shapeToRay = new(localRay.origin - origin);

            double a = Vector.DotProduct(localRay.direction, localRay.direction);
            double b = 2 * Vector.DotProduct(localRay.direction, shapeToRay);
            double c = Vector.DotProduct(shapeToRay, shapeToRay) - 1;
            double discriminant = b * b - 4 * a * c;

            if (discriminant >= 0)
            {
                double sqrtD = Sqrt(discriminant);
                double t1 = (-b + sqrtD) / (2 * a);
                double t2 = (-b - sqrtD) / (2 * a);

                yield return new Intersection(t1, this);
                yield return new Intersection(t2, this);
            }
            
            yield break;
        }
    }
}
