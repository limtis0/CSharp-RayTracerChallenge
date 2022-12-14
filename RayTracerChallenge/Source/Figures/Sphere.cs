using RT.Source.Materials;
using RT.Source.Matrices;
using RT.Source.Vectors;

namespace RT.Source.Figures
{
    public class Sphere : Figure
    {
        public const double Radius = 1;

        public Sphere()
        {
            material = new();
        }

        public Sphere(Material material)
        {
            this.material = material;
        }

        public override Vector NormalAt(Point p)
        {
            Matrix inv = transform.Inverse();
            Point objPoint = new(inv * p);
            Vector objNormal = new(objPoint - origin);

            // This is a little bit of a hack; Everything, except W is computed correctly;
            // To get rid of this, you either need to have complex operations, or W setted to 0;
            Vectors.Tuple worldNormal = inv.Transposed() * objNormal;
            worldNormal.w = 0;

            return Vector.Normalize(new Vector(worldNormal));
        }

        public override string ToString() => $"{GetType().Name} {id}";
    }
}
