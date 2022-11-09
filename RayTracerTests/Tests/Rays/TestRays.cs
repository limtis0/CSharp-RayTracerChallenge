using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Vectors;
using RT.Source.Rays;

namespace RayTracerTests
{
    [TestClass]
    public class TestRays
    {
        [TestMethod]
        public void TestCreation()
        {
            Point origin = new(1, 2, 3);
            Vector direction = new(4, 5, 6);
            Ray ray = new (origin, direction);

            Assert.AreEqual(ray.origin, origin);
            Assert.AreEqual(ray.direction, direction);
        }

        [TestMethod]
        public void TestPosition()
        {
            Point origin = new(2, 3, 4);
            Vector direction = new(1, 0, 0);
            Ray ray = new(origin, direction);

            Assert.AreEqual(ray.Position(0), origin);
            Assert.AreEqual(ray.Position(1), new Point(3, 3, 4));
            Assert.AreEqual(ray.Position(-1), new Point(1, 3, 4));
            Assert.AreEqual(ray.Position(2.5f), new Point(4.5f, 3, 4));
        }
    }
}
