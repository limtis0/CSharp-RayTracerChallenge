using RT.Source.Figures;
using RT.Source.Light;
using RT.Source.Materials;
using RT.Source.Draw;
using RT.Source.Vectors;
using RT.Source.Rays;

namespace RT.Source.World
{
    public sealed class World
    {
        #region Singleton

        private static readonly World instance = new();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static World()
        {
        }

        private World()
        {
        }

        public static World Instance => instance;

        #endregion

        public List<Figure> figures = new();
        public List<PointLight> lights = new();  // TODO: Light base class

        // For tests
        internal static World DefaultWorld()
        {
            Sphere sphere1 = new()
            {
                material = new Material(
                new Color(0.8f, 1f, 0.6f),
                diffuse: 0.7f,
                specular: 0.2f
            )
            };

            Sphere sphere2 = new();
            sphere2.transform = sphere2.transform.Scale(0.5f, 0.5f, 0.5f);

            PointLight light = new(new Point(-10, 10, -10), new Color(1, 1, 1));

            Instance.figures = new() { sphere1, sphere2 };
            Instance.lights = new() { light };

            return Instance;
        }

        #region Rendering

        internal Intersections Intersect(Ray r) => r.IntersectionsWith(figures);

        internal Color ShadeHit(Precomputations comps)
        {
            bool inShadow = IsShadowed(comps.overPoint);

            Color result = new();
            foreach (PointLight light in lights)
                result += ShadeHitForLight(comps, light, inShadow);

            return result;
        }

        private static Color ShadeHitForLight(Precomputations c, PointLight l, bool inShadow) 
            => c.figure.material.Lighting(l, c.overPoint, c.eyeV, c.normalV, inShadow);

        public Color ColorAt(Ray ray)
        {
            Intersections intersections = Intersect(ray);
            Intersection? hit = intersections.Hit();
            
            if (hit is null)
                return new Color();

            Precomputations comps = hit.PrepareComputations(ray);

            return ShadeHit(comps);
        }

        internal bool IsShadowed(Point point)
        {
            foreach (PointLight light in lights)
            {
                if (IsPointFromLightShadowed(point, light))
                    return true;
            }
            return false;
        }

        private bool IsPointFromLightShadowed(Point point, PointLight light)
        {
            Vector vector = new(light.position - point);
            float distance = Vector.Magnitude(vector);
            Vector direction = Vector.Normalize(vector);

            Ray ray = new(point, direction);
            Intersections intersections = Intersect(ray);

            Intersection? hit = intersections.Hit();
            if (hit is not null && hit.T < distance)
                return true;

            return false;
        }

        #endregion
    }
}
