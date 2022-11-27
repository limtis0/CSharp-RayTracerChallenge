using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Figures;
using RT.Source.Rays;

namespace RayTracerTests
{
    [TestClass]
    public class TestIntersections
    {
        [TestMethod]
        public void TestHit()
        {
            Sphere s = new();

            // All intersections have positive T
            Intersection i1p = new(1, s);
            Intersection i2p = new(2, s);
            Intersections allPos = new();
            allPos.Insert(i1p, i2p);  // 1, 2

            Assert.AreEqual(allPos.Hit(), i1p);


            // Some intersections have negative T
            Intersection i1n = new(-1, s);
            Intersections someNeg = new();
            someNeg.Insert(i1n, i1p);  // -1, 1

            Assert.AreEqual(someNeg.Hit(), i1p);


            // All intersections have negative T
            Intersection i2n = new(-2, s);
            Intersections allNeg = new();
            allNeg.Insert(i1n, i2n);  // -1, -2

            Assert.IsNull(allNeg.Hit());


            // General condition
            Intersection r1 = new(5, s);
            Intersection r2 = new(7, s);
            Intersection r3 = new(-3, s);
            Intersection r4 = new(2, s);
            Intersections general = new();
            general.Insert(r1, r2, r3, r4);

            Assert.AreEqual(general.Hit(), r4);
        }
    }
}
