using RT.Source.Matrices;
using RT.Source.Vectors;
using RT.Source.Materials;
using RT.Source.Rays;

namespace RT.Source.Figures
{
    public abstract class Figure
    {
        private static int globalId = 0;
        public readonly int id = globalId++;

        public readonly Point origin = new(0, 0, 0);
        public Matrix transform = Matrix.Identity();
        public Material material = new();


        protected abstract Vector LocalNormalAt(Point point);

        public Vector NormalAt(Point p)
        {
            Matrix inverse = transform.Inverse();

            Point localPoint = new(inverse * p);
            Vector localNormal = LocalNormalAt(localPoint);

            Vectors.Tuple worldNormal = inverse.Transposed() * localNormal;
            worldNormal.w = 0;

            return Vector.Normalize(new Vector(worldNormal));
        }

        protected abstract IEnumerable<Intersection> LocalIntersect(Ray localRay);

        public IEnumerable<Intersection> Intersect(Ray ray)
        {
            Ray localRay = ray.Transform(transform.Inverse());
            return LocalIntersect(localRay);
        }

        public override string ToString() => $"{GetType().Name} {id}";
    }
}
