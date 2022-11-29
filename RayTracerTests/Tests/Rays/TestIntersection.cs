using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Figures;
using RT.Source.Rays;
using RT.Source.Vectors;

namespace RayTracerTests
{
    [TestClass]
    public class TestIntersection
    {
        [TestMethod]
        public void TestPrecomputations()
        {
            Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere s = new();
            Intersection i = new(4, s);

            Precomputations comps = i.PrepareComputations(r);

            Assert.AreEqual(comps.T, i.T);
            Assert.AreEqual(comps.figure, i.figure);
            Assert.AreEqual(comps.point, new Point(0, 0, -1));
            Assert.AreEqual(comps.eyeV, new Vector(0, 0, -1));
            Assert.AreEqual(comps.normalV, new Vector(0, 0, -1));
        }

        [TestMethod]
        public void TestPrecompsIntersectionInside()
        {
            // Outside intersection
            Ray r1 = new(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere s = new();
            Intersection i1 = new(4, s);

            Precomputations comps1 = i1.PrepareComputations(r1);

            Assert.IsFalse(comps1.inside);


            // Inside intersection
            Ray r2 = new(new Point(0, 0, 0), new Vector(0, 0, 1));
            Intersection i2 = new(1, s);

            Precomputations comps2 = i2.PrepareComputations(r2);

            Assert.AreEqual(comps2.point, new Point(0, 0, 1));
            Assert.AreEqual(comps2.eyeV, new Vector(0, 0, -1));
            Assert.IsTrue(comps2.inside);
            Assert.AreEqual(comps2.normalV, new Vector(0, 0, -1));  // Is inverted
        }
    }
}
