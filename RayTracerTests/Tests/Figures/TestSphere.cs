using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Figures;
using RT.Source.Matrices;
using RT.Source.Rays;
using RT.Source.Vectors;

namespace RayTracerTests
{
    [TestClass]
    public class TestSphere
    {
        [TestMethod]
        public void TestCreation()
        {
            Sphere s = new();
            Assert.AreEqual(s.transform, Matrix.Identity());
        }

        [TestMethod]
        public void TestTransform()
        {
            Sphere s = new();
            Matrix t = Matrix.Translation(2, 3, 4);
            s.transform = s.transform.Translate(2, 3, 4);

            Assert.AreEqual(s.transform, t);
        }

        [TestMethod]
        public void TestIntersect()
        {
            // Intersecting a scaled sphere with a ray
            Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere s = new();
            s.transform = s.transform.Scale(2, 2, 2);
            Intersections xs = r.IntersectionsWith(s);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].T, 3);
            Assert.AreEqual(xs[1].T, 7);

            // Intersecting a translated sphere with a ray
            s.transform = s.transform.Translate(5, 0, 0);
            Intersections xt = r.IntersectionsWith(s);
            Assert.AreEqual(xt.Count, 0);
        }
    }
}
