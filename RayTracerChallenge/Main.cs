using RT.Source.World;
using RT.Source.Light;
using RT.Source.Vectors;
using RT.Source.Matrices;
using RT.Source.Figures;
using RT.Source.Draw;
using static System.Math;

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
                transform = Matrix.Scaling(10, 0.01, 10)
            };
            floor.material.color = new Color(1, 0.9, 0.9);
            floor.material.specular = 0;
            world.figures.Add(floor);

            // Left wall
            Sphere leftWall = new()
            {
                transform = Matrix.Scaling(10, 0.01, 10).RotateX(PI / 2).RotateY(-PI / 4).Translate(0, 0, 5),
                material = floor.material
            };
            world.figures.Add(leftWall);

            // Right wall
            Sphere rightWall = new()
            {
                transform = Matrix.Scaling(10, 0.01, 10).RotateX(PI / 2).RotateY(PI / 4).Translate(0, 0, 5),
                material = floor.material
            };
            world.figures.Add(rightWall);

            // Middle sphere
            Sphere middle = new()
            {
                transform = Matrix.Translation(-0.5, 1, 0.5)
            };
            middle.material.color = new Color(0.1, 1, 0.5);
            middle.material.diffuse = 0.7;
            middle.material.specular = 0.3;
            world.figures.Add(middle);

            // Right sphere
            Sphere right = new()
            {
                transform = Matrix.Scaling(0.5, 0.5, 0.5).Translate(1.5, 0.5, -0.5)
            };
            right.material.color = new Color(0.5, 1, 0.1);
            right.material.diffuse = 0.7;
            right.material.specular = 0.3;
            world.figures.Add(right);

            // Left sphere
            Sphere left = new()
            {
                transform = Matrix.Scaling(0.33, 0.33, 0.33).Translate(-1.5, 0.33, -0.75) 
            };
            left.material.color = new Color(1, 0.8, 0.1);
            left.material.diffuse = 0.7;
            left.material.specular = 0.3;
            world.figures.Add(left);

            // Camera
            Camera camera = new(768, 1024, PI / 3)
            {
                transform = Matrix.ViewTransformation(
                new Point(0, 1.5, -7),
                new Point(-1.5, 0, 0.5),
                new Vector(0, 1, 0)
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
