using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Figures;
using RT.Source.Vectors;
using RT.Source.Rays;

namespace RayTracerTests
{
    [TestClass]
    public class TestPlanes
    {
        [TestMethod]
        public void TestNormalAt()
        {
            Plane p = new();
            Vector normal = new(0, 1, 0);

            // Normal is constant
            Assert.AreEqual(p.NormalAt(new Point(0, 0, 0)), normal);
            Assert.AreEqual(p.NormalAt(new Point(-10, 0, -10)), normal);
            Assert.AreEqual(p.NormalAt(new Point(-5, 0, 150)), normal);
        }

        [TestMethod]
        public void TestIntersect()
        {
            Plane p = new();


            // Intersect with a ray parallel to the plane
            Ray parallelRay = new(new Point(0, 10, 0), new Vector(0, 0, 1));
            Intersections parallelXS = parallelRay.IntersectionsWith(p);

            Assert.AreEqual(parallelXS.Count, 0);


            // Intersect with a ray coplanar to the plane
            Ray coplanarRay = new(new Point(0, 0, 0), new Vector(0, 0, 1));
            Intersections coplanarXS = coplanarRay.IntersectionsWith(p);

            Assert.AreEqual(coplanarXS.Count, 0);


            // Intersect with a ray from above
            Ray aboveRay = new(new Point(0, 1, 0), new Vector(0, -1, 0));
            Intersections aboveXS = aboveRay.IntersectionsWith(p);

            Assert.AreEqual(aboveXS.Count, 1);
            Assert.AreEqual(aboveXS[0].T, 1);
            Assert.AreEqual(aboveXS[0].figure, p);


            // Intersect with a ray from below
            Ray belowRay = new(new Point(0, -1, 0), new Vector(0, 1, 0));
            Intersections belowXS = belowRay.IntersectionsWith(p);

            Assert.AreEqual(belowXS.Count, 1);
            Assert.AreEqual(belowXS[0].T, 1);
            Assert.AreEqual(belowXS[0].figure, p);
        }
    }
}
