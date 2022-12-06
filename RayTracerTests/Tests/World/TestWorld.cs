using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Draw;
using RT.Source.Figures;
using RT.Source.Light;
using RT.Source.Matrices;
using RT.Source.Rays;
using RT.Source.Vectors;
using RT.Source.World;

namespace RayTracerTests
{
    // Tests may break because of Singleton pattern,
    // If so, run the broken ones separately :)
    [TestClass]
    public class TestWorld
    {
        [TestMethod]
        public void TestCreation()
        {
            // Adding items
            World world = World.Instance;

            Sphere s = new();
            world.figures.Add(s);

            PointLight l = new(new Point(-10, 10, -10), new Color(1, 1, 1));
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
            Assert.AreEqual(xs[1].T, 4.5);
            Assert.AreEqual(xs[2].T, 5.5);
            Assert.AreEqual(xs[3].T, 6);
        }

        [TestMethod]
        public void TestShadeHit()
        {
            // Shading an intersection
            World w = World.DefaultWorld();
            Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
            Figure f = w.figures[0];
            Intersection i = new(4, f);

            Precomputations comps = i.PrepareComputations(r);

            Assert.AreEqual(w.ShadeHit(comps), new Color(0.38066, 0.47583, 0.2855));


            // Shading an intersection from inside
            w.lights = new() { new PointLight(new Point(0, 0.25, 0), new Color(1, 1, 1)) };
            r = new(new Point(0, 0, 0), new Vector(0, 0, 1));
            f = w.figures[1];
            i = new(0.5, f);

            comps = i.PrepareComputations(r);

            Assert.AreEqual(w.ShadeHit(comps), new Color(0.90498, 0.90498, 0.90498));
        }

        [TestMethod]
        public void TestShadeHitInShadow()
        {
            World w = World.Instance;

            w.lights.Add(new PointLight(new Point(0, 0, -10), new Color(1, 1, 1)));

            Sphere s = new() { transform = Matrix.Translation(0, 0, 10) };
            w.figures.Add(new Sphere());
            w.figures.Add(s);

            Ray r = new(new Point(0, 0, 5), new Vector(0, 0, 1));
            Intersection i = new(4, s);

            Precomputations comps = i.PrepareComputations(r);

            Assert.AreEqual(w.ShadeHit(comps), new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestColorAt()
        {
            World w = World.DefaultWorld();

            // Ray misses
            Ray r = new(new Point(0, 0, -5), new Vector(0, 1, 0));

            Assert.AreEqual(w.ColorAt(r), new Color(0, 0, 0));


            // Ray hits
            r = new(new Point(0, 0, -5), new Vector(0, 0, 1));

            Assert.AreEqual(w.ColorAt(r), new Color(0.38066, 0.47583, 0.2855));


            // Intersection is behind the ray
            Figure outer = w.figures[0];
            outer.material.ambient = 1;

            Figure inner = w.figures[1];
            inner.material.ambient = 1;

            r = new(new Point(0, 0, 0.75), new Vector(0, 0, -1));

            Assert.AreEqual(w.ColorAt(r), inner.material.color);
        }

        [TestMethod]
        public void TestIsShadowed()
        {
            World w = World.DefaultWorld();

            // Nothing is collinear with point and light
            Assert.IsFalse(w.IsShadowed(new Point(0, 10, 0)));


            // Object between point and light
            Assert.IsTrue(w.IsShadowed(new Point(10, -10, 10)));


            // Object is behind the light
            Assert.IsFalse(w.IsShadowed(new Point(-20, 20, -20)));


            // Object is behind the point
            Assert.IsFalse(w.IsShadowed(new Point(-2, 2, -2)));
        }
    }
}
