using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Draw;
using RT.Source.Light;
using RT.Source.Vectors;

namespace RayTracerTests
{
    [TestClass]
    public class TestLight
    {
        [TestMethod]
        public void TestCreation()
        {
            Point p = new(0, 0, 0);
            Color c = new(1, 1, 1);
            PointLight light = new(p, c);

            Assert.AreEqual(light.position, p);
            Assert.AreEqual(light.intensity, c);
        }
    }
}
