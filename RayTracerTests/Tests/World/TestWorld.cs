using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.World;
using RT.Source.Figures;
using RT.Source.Light;
using RT.Source.Vectors;
using RT.Source.Draw;
using RT.Source.Rays;

namespace RayTracerTests
{
    [TestClass]
    public class TestWorld
    {
        [TestMethod]
        public void TestCreation()
        {
            // Default world is empty
            World world = World.Instance;
            Assert.AreEqual(world.figures.Count, 0);
            Assert.AreEqual(world.lights.Count, 0);

            // Adding items
            Sphere s = new();
            PointLight l = new(new Point(-10, 10, -10), new Color(1, 1, 1));
            world.figures.Add(s);
            world.lights.Add(l);
            Assert.IsTrue(world.figures.Contains(s));
            Assert.IsTrue(world.lights.Contains(l));
        }

        [TestMethod]
        public void TestIntersection()
        {
            World world = World.DefaultWorld();
            Ray ray = new(new Point(0, 0, -5), new Vector(0, 0, 1));
            
            Intersections xs = world.Intersect(ray);

            Assert.AreEqual(xs.Count, 4);
            Assert.AreEqual(xs[0].T, 4);
            Assert.AreEqual(xs[1].T, 4.5f);
            Assert.AreEqual(xs[2].T, 5.5f);
            Assert.AreEqual(xs[3].T, 6);
        }
    }
}
