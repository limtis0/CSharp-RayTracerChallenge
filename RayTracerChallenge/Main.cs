using RT.Source.World;
using RT.Source.Light;
using RT.Source.Vectors;
using RT.Source.Matrices;
using RT.Source.Figures;
using RT.Source.Draw;
using static System.MathF;

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
            World world = World.Instance;

            // Floor
            Sphere floor = new()
            {
                transform = Matrix.Scaling(10, 0.01f, 10)
            };
            floor.material.color = new Color(1, 0.9f, 0.9f);
            floor.material.specular = 0;
            world.figures.Add(floor);

            // Left wall
            Sphere leftWall = new()
            {
                transform = Matrix.Scaling(10, 0.01f, 10).RotateX(PI / 2).RotateY(-PI / 4).Translate(0, 0, 5),
                material = floor.material
            };
            world.figures.Add(leftWall);

            // Right wall
            Sphere rightWall = new()
            {
                transform = Matrix.Scaling(10, 0.01f, 10).RotateX(PI / 2).RotateY(PI / 4).Translate(0, 0, 5),
                material = floor.material
            };
            world.figures.Add(rightWall);

            // Middle sphere
            Sphere middle = new()
            {
                transform = Matrix.Translation(-0.5f, 1, 0.5f)
            };
            middle.material.color = new Color(0.1f, 1, 0.5f);
            middle.material.diffuse = 0.7f;
            middle.material.specular = 0.3f;
            world.figures.Add(middle);

            // Right sphere
            Sphere right = new()
            {
                transform = Matrix.Scaling(0.5f, 0.5f, 0.5f).Translate(1.5f, 0.5f, -0.5f)
            };
            right.material.color = new Color(0.5f, 1, 0.1f);
            right.material.diffuse = 0.7f;
            right.material.specular = 0.3f;
            world.figures.Add(right);

            // Left sphere
            Sphere left = new()
            {
                transform = Matrix.Scaling(0.33f, 0.33f, 0.33f).Translate(-1.5f, 0.33f, -0.75f) 
            };
            left.material.color = new Color(1, 0.8f, 0.1f);
            left.material.diffuse = 0.7f;
            left.material.specular = 0.3f;
            world.figures.Add(left);

            // Camera
            Camera camera = new(200, 400, PI / 3)
            {
                transform = Matrix.ViewTransformation(
                new Point(0, 1.5f, -7),
                new Point(-1.5f, 0, 0.5f),
                new Vector(0, 1f, 0)
                )
            };

            // Light
            PointLight light = new(new Point(-10, 10, -10), new Color(1, 1, 1));
            world.lights.Add(light);

            Canvas render = camera.Render();

            render.ToPPM("world_shadows");
        }
    }
}
