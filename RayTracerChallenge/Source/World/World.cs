using System.Runtime.CompilerServices;
using RT.Source.Figures;
using RT.Source.Light;
using RT.Source.Materials;
using RT.Source.Draw;
using RT.Source.Vectors;
using RT.Source.Rays;

[assembly: InternalsVisibleTo("RayTracerTests")]
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
            Sphere s1 = new();
            s1.material = new Material(
                new Color(0.8f, 1f, 0.6f),
                diffuse: 0.7f,
                specular: 0.2f
            );

            Sphere s2 = new();
            s2.transform = s2.transform.Scale(0.5f, 0.5f, 0.5f);

            PointLight l1 = new(new Point(-10, 10, -10), new Color(1, 1, 1));

            Instance.figures = new() { s1, s2 };
            Instance.lights = new() { l1 };

            return Instance;
        }

        internal Intersections Intersect(Ray r) => r.IntersectionsWith(figures);

        internal Color ShadeHit(Precomputations comps)
        {
            Color result = new();
            foreach (PointLight l in lights)
                result += ShadeHitForLight(comps, l);

            return result;
        }

        private static Color ShadeHitForLight(Precomputations c, PointLight l) => c.figure.material.Lighting(l, c.point, c.eyeV, c.normalV);

        public Color ColorAt(Ray r)
        {
            Intersections xs = Intersect(r);
            Intersection? hit = xs.Hit();
            
            if (hit is null)
                return new Color();

            Precomputations comps = hit.PrepareComputations(r);

            return ShadeHit(comps);
        }
    }
}
