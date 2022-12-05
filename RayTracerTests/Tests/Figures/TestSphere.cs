using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Draw;
using RT.Source.Figures;
using RT.Source.Materials;
using RT.Source.Matrices;
using RT.Source.Rays;
using RT.Source.Vectors;
using static System.MathF;

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

        [TestMethod]
        public void TestNormalAtSimple()
        {
            Sphere s = new();

            // Normal on the X axis
            Point p1 = new(1, 0, 0);
            Vector v1 = new(1, 0, 0);

            Assert.AreEqual(s.NormalAt(p1), v1);


            // Normal on the Y axis
            Point p2 = new(0, 1, 0);
            Vector v2 = new(0, 1, 0);

            Assert.AreEqual(s.NormalAt(p2), v2);


            // Normal on the Z axis
            Point p3 = new(0, 0, 1);
            Vector v3 = new(0, 0, 1);

            Assert.AreEqual(s.NormalAt(p3), v3);


            // Normal at non-axial point
            float r3d3 = Sqrt(3) / 3;
            Point p4 = new(r3d3, r3d3, r3d3);
            Vector v4 = new(r3d3, r3d3, r3d3);
            Vector n = s.NormalAt(p4);

            Assert.AreEqual(n, v4);


            // Is normalized
            Assert.AreEqual(n, Vector.Normalize(n));
        }

        [TestMethod]
        public void TestNormalAtComplex()
        {
            // Normal on a translated sphere
            Sphere translated = new();
            translated.transform = translated.transform.Translate(0, 1, 0);
            Vector n1 = translated.NormalAt(new Point(0, 1.70711f, -0.70711f));
            Assert.AreEqual(n1, new Vector(0, 0.70711f, -0.70711f));

            // Normal on a transformed sphere
            Sphere transposed = new();
            transposed.transform = transposed.transform
                .RotateZ(PI / 5)
                .Scale(1, 0.5f, 1);
            float r2d2 = Sqrt(2) / 2;
            Vector n2 = transposed.NormalAt(new Point(0, r2d2, -r2d2));
            Assert.AreEqual(n2, new Vector(0, 0.97014f, -0.24254f));
        }

        [TestMethod]
        public void TestMaterial()
        {
            // Sphere has a default material
            Sphere s = new();

            Assert.AreEqual(s.material, new Material());


            // Sphere's materials can be changed
            Material m = new(new Color(1, 1, 1), 0.7f, 0.9f, 0.2f, 60f);
            s.material = m;

            Assert.AreEqual(s.material, m);
        }
    }
}
