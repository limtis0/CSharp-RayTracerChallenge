﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Figures;
using RT.Source.Rays;
using RT.Source.Vectors;

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
            Ray ray = new(origin, direction);

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

        [TestMethod]
        public void TestIntersect()
        {
            // Keep in mind, sphere center is always on P(0, 0, 0) and Radius always equals 1
            Sphere s = new();

            // Intersect the sphere at two points
            Ray r1 = new(new Point(0, 0, -5), new Vector(0, 0, 1));
            Intersections i1 = r1.IntersectionsWith(s);

            Assert.AreEqual(i1.Count, 2);
            Assert.AreEqual(i1[0].T, 4f);
            Assert.AreEqual(i1[1].T, 6f);

            // Assert sphere reference is saved
            Assert.AreEqual(i1[0].Figure, s);
            Assert.AreEqual(i1[1].Figure, s);

            // Intersect the sphere at a tangent
            Ray r2 = new(new Point(0, Sphere.Radius, -5), new Vector(0, 0, 1));
            Intersections i2 = r2.IntersectionsWith(s);

            Assert.AreEqual(i2.Count, 2);
            Assert.AreEqual(i2[0].T, 5f);
            Assert.AreEqual(i2[1].T, 5f);

            // Miss the sphere
            Ray r3 = new(new Point(0, Sphere.Radius * 2, -5), new Vector(0, 0, 1));
            Intersections i3 = r3.IntersectionsWith(s);

            Assert.AreEqual(i3.Count, 0);

            // A ray originates inside the sphere
            Ray r4 = new(Sphere.Center, new Vector(0, 0, 1));
            Intersections i4 = r4.IntersectionsWith(s);

            Assert.AreEqual(i4.Count, 2);
            Assert.AreEqual(i4[0].T, -1f);
            Assert.AreEqual(i4[1].T, 1f);

            // The sphere is behind a ray
            Ray r5 = new(new Point(0, 0, 5), new Vector(0, 0, 1));
            Intersections i5 = r5.IntersectionsWith(s);

            Assert.AreEqual(i5.Count, 2);
            Assert.AreEqual(i5[0].T, -6f);
            Assert.AreEqual(i5[1].T, -4f);
        }
    }
}
