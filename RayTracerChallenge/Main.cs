using RT.Source.Draw;
using RT.Source.Figures;
using RT.Source.Light;
using RT.Source.Rays;
using RT.Source.Vectors;

// DO NOT DELETE! THIS ALLOWS TESTING ON INTERNALS.
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("RayTracerTests")]
// DO NOT DELETE!

namespace RT
{
    class RayTracerChallenge
    {
        private static void Main()
        {
            // Setup of a camera (ray origin) and a "wall" to project to
            Point rayOrigin = new(0, 0, -5);
            const int wallZ = 10;
            const float wallSize = 7;
            const float halfWallSize = wallSize / 2;

            // Canvas setup
            const int canvasSize = 512;
            const float pixelSize = wallSize / canvasSize;
            Canvas canvas = new(canvasSize, canvasSize);

            // Sphere transformations
            Sphere s = new();
            s.transform = s.transform
                .Scale(0.5f, 1, 1)
                .RotateZ(0.52f);
            s.material.color = new Color(0, 1f, 0.1f);

            // Light setup
            Point lightPosition = new(-10, 10, -10);
            Color lightColor = new(1, 1, 1);
            PointLight light = new(lightPosition, lightColor);

            for (int y = 0; y < canvasSize; y++)
            {
                // World Y coordinate (top = +half; bottom = -half)
                float worldY = halfWallSize - pixelSize * y;
                for (int x = 0; x < canvasSize; x++)
                {
                    // World X coordinate (left = -half; right = +half)
                    float worldX = -halfWallSize + pixelSize * x;

                    // Point for the ray to target
                    Point position = new(worldX, worldY, wallZ);

                    Ray r = new(rayOrigin, Vector.Normalize(new Vector(position - rayOrigin)));
                    Intersections xs = r.IntersectionsWith(s);

                    Intersection? hit = xs.Hit();
                    if (hit is not null)
                    {
                        Point hitPos = r.Position(hit.T);
                        Vector normal = ((Sphere)hit.figure).NormalAt(hitPos);
                        Vector eye = new(-r.direction);
                        Color color = ((Sphere)hit.figure).material.Lighting(light, hitPos, eye, normal);

                        canvas.SetPixel(y, x, color);
                    }
                }
            }

            canvas.ToPPM();
        }
    }
}
