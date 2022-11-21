using RT.Source.Draw;
using RT.Source.Figures;
using RT.Source.Rays;
using RT.Source.Vectors;

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
            const int canvasSize = 256;
            const float pixelSize = wallSize / canvasSize;
            Canvas canvas = new(canvasSize, canvasSize);
            Color white = new(1, 1, 1);

            // Sphere transformations
            Sphere s = new();
            s.transform = s.transform
                .Scale(1, 0.5f, 1);

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

                    if (xs.Hit() is not null)
                        canvas.SetPixel(y, x, white);
                }
            }

            canvas.ToPPM();
        }
    }
}
