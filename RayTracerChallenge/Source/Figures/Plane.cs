using RT.Source.Rays;
using RT.Source.Vectors;
using static RT.Source.Calc;

namespace RT.Source.Figures
{
    public class Plane : Figure
    {
        private static readonly Vector constNormal = new(0, 1, 0);

        protected override Vector LocalNormalAt(Point point) => constNormal;  // The normal of a plane is constant everywhere

        protected override IEnumerable<Intersection> LocalIntersect(Ray localRay)
        {
            if (Calc.Equals(localRay.direction.y, 0))
                yield break;

            double T = -localRay.origin.y / localRay.direction.y;
            yield return new Intersection(T, this);
        }
    }
}
