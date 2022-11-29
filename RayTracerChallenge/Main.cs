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
            Sphere floor = new();
            floor.transform = Matrix.Scaling(10, 0.01f, 10);
            floor.material.color = new Color(1, 0.9f, 0.9f);
            floor.material.specular = 0;
            world.figures.Add(floor);

            // Left wall
            Sphere leftWall = new();
            leftWall.transform = leftWall.transform
                .Translate(0, 0, 5)
                .RotateY(-PI / 4)
                .RotateX(PI / 2)
                .Scale(10, 0.01f, 10);

            leftWall.material = floor.material;
            world.figures.Add(leftWall);

            // Right wall
            Sphere rightWall = new();
            rightWall.transform = rightWall.transform
                .Translate(0, 0, 5)
                .RotateY(PI / 4)
                .RotateX(PI / 2)
                .Scale(10, 0.01f, 10);
            rightWall.material = floor.material;
            world.figures.Add(rightWall);

            // Middle sphere
            Sphere middle = new();
            middle.transform = Matrix.Translation(-0.5f, 1, 0.5f);
            middle.material.color = new Color(0.1f, 1, 0.5f);
            middle.material.diffuse = 0.7f;
            middle.material.specular = 0.3f;
            world.figures.Add(middle);

            // Right sphere
            Sphere right = new();
            right.transform = right.transform
                .Translate(1.5f, 0.5f, -0.5f)
                .Scale(0.5f, 0.5f, 0.5f);
            right.material.color = new Color(0.5f, 1, 0.1f);
            right.material.diffuse = 0.7f;
            right.material.specular = 0.3f;
            world.figures.Add(right);

            // Left sphere
            Sphere left = new();
            left.transform = left.transform
                .Translate(-1.5f, 0.33f, -0.75f)
                .Scale(0.33f, 0.33f, 0.33f);
            left.material.color = new Color(1, 0.8f, 0.1f);
            left.material.diffuse = 0.7f;
            left.material.specular = 0.3f;
            world.figures.Add(left);

            // Camera
            Camera camera = new(768, 1024, PI / 3);
            camera.transform = Matrix.ViewTransformation(
                new Point(0, 1.5f, -5),
                new Point(0, 1, 0),
                new Vector(0, 1, 0)
                );

            // Light
            PointLight light = new(new Point(-10, 10, -10), new Color(1, 1, 1));
            world.lights.Add(light);

            Canvas render = camera.Render();

            render.ToPPM("world");
        }
    }
}
