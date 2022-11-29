using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.World;
using RT.Source.Matrices;
using RT.Source.Rays;
using RT.Source.Vectors;
using RT.Source.Draw;
using static System.MathF;

namespace RayTracerTests
{
    [TestClass]
    public class TestCamera
    {
        [TestMethod]
        public void TestCreation()
        {
            int hSize = 160;
            int wSize = 160;
            float fov = PI / 2;

            Camera camera = new(hSize, wSize, fov);

            Assert.AreEqual(camera.canvasHeight, hSize);
            Assert.AreEqual(camera.canvasWidth, wSize);
            Assert.AreEqual(camera.fov, fov);
            Assert.AreEqual(camera.transform, Matrix.Identity());
        }

        [TestMethod]
        public void TestPixelSize()
        {
            // For horizontal canvas
            Camera camera = new(200, 125, PI / 2);

            Assert.AreEqual(camera.pixelSize, 0.01f);


            // For vertical canvas
            camera = new(125, 200, PI / 2);

            Assert.AreEqual(camera.pixelSize, 0.01f);
        }

        [TestMethod]
        public void TestRayForPixel()
        {
            // Ray through center of canvas
            Camera c = new(201, 101, PI/2);
            Ray r = c.RayForPixel(50, 100);

            Assert.AreEqual(r.origin, new Point(0, 0, 0));
            Assert.AreEqual(r.direction, new Vector(0, 0, -1));


            // Ray through corner
            r = c.RayForPixel(0, 0);

            Assert.AreEqual(r.origin, new Point(0, 0, 0));
            Assert.AreEqual(r.direction, new Vector(0.66519f, 0.33259f, -0.66851f));


            // With transformed camera
            float r2d2 = Sqrt(2) / 2;
            c.transform = c.transform.RotateY(PI / 4).Translate(0, -2, 5);
            r = c.RayForPixel(50, 100);

            Assert.AreEqual(r.origin, new Point(0, 2, -5));
            Assert.AreEqual(r.direction, new Vector(r2d2, 0, -r2d2));
        }

        [TestMethod]
        public void TestRender()
        {
            World w = World.DefaultWorld();
            
            Camera c = new(11, 11, PI / 2);
            Point from = new(0, 0, -5);
            Point to = new(0, 0, 0);
            Vector up = new(0, 1, 0);
            c.transform = Matrix.ViewTransformation(from, to, up);

            Canvas image = c.Render();

            Assert.AreEqual(image.GetPixel(5, 5), new Color(0.38066f, 0.47583f, 0.2855f));
        }
    }
}
