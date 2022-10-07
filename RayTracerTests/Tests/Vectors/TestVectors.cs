using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Vectors;
using RT.Source;

namespace RayTracerTests
{
    [TestClass]
    public class TestCoordinates
    {
        [TestMethod]
        public void TestIsPointAndIsVector()
        {
            // Point
            Point p1 = new(1, 2, 3);
            Assert.IsTrue(p1.IsPoint());
            Assert.IsFalse(p1.IsVector());

            // Vector
            Vector v1 = new(3, 2, 1);
            Assert.IsFalse(v1.IsPoint());
            Assert.IsTrue(v1.IsVector());

            // Tuple (Point)
            Tuple t1 = new(4.3f, -4.2f, 3.1f, 1.0f);
            Assert.IsTrue(t1.IsPoint());
            Assert.IsFalse(t1.IsVector());

            // Tuple (Vector)
            Tuple t2 = new(4.3f, -4.2f, 3.1f, 0.0f);
            Assert.IsFalse(t2.IsPoint());
            Assert.IsTrue(t2.IsVector());
        }

        [TestMethod]
        public void TestAddition()
        {
            Tuple t1 = new(3, -2, 5, 1);
            Tuple t2 = new(-2, 3, 1, 0);
            Tuple expected = new(1, 1, 6, 1);
            Assert.AreEqual(t1 + t2, expected);
        }

        [TestMethod]
        public void TestSubtract()
        {
            // Subtracting two points
            Point p1 = new(3, 2, 1);
            Point p2 = new(5, 6, 7);
            Vector expected1 = new(-2, -4, -6);
            Assert.AreEqual(p1 - p2, expected1);

            // Subtracting vector from a point
            Vector v1 = new(5, 6, 7);
            Point expected2 = new(-2, -4, -6);
            Assert.AreEqual(p1 - v1, expected2);

            // Subrtacting vector from vector
            Vector v2 = new(3, 2, 1);
            Vector expected3 = new(-2, -4, -6);
            Assert.AreEqual(v2 - v1, expected3);
        }

        [TestMethod]
        public void TestNegate()
        {
            Tuple t1 = new(1, -2, 3, -4);
            Tuple expected = new(-1, 2, -3, 4);
            Assert.AreEqual(-t1, expected);
        }

        [TestMethod]
        public void TestScalarMultiply()
        {
            // By scalar (x2)
            Tuple t1 = new(1, -2, 3, -4);
            Tuple expected1 = new(3.5f, -7, 10.5f, -14);
            Assert.AreEqual(t1 * 3.5f, expected1);

            // By fraction (x0.5)
            Tuple expected2 = new(0.5f, -1, 1.5f, -2);
            Assert.AreEqual(t1 * 0.5f, expected2);
        }

        [TestMethod]
        public void TestScalarDivision()
        {
            Tuple t1 = new(1, -2, 3, -4);
            Tuple expected = new(0.5f, -1, 1.5f, -2);
            Assert.AreEqual(t1 / 2, expected);
        }

        [TestMethod]
        public void TestMagnitude()
        {
            // Order independency
            Vector v1 = new(1, 0, 0);
            Vector v2 = new(0, 1, 0);
            Vector v3 = new(0, 0, 1);
            float expected1 = 1;
            Assert.AreEqual(Vector.Magnitude(v1), expected1);
            Assert.AreEqual(Vector.Magnitude(v2), expected1);
            Assert.AreEqual(Vector.Magnitude(v3), expected1);

            // Absolute values
            Vector v4 = new(1, 2, 3);
            Vector v5 = new(-1, -2, -3);
            float expected2 = (float)System.Math.Sqrt(14);
            Assert.AreEqual(Vector.Magnitude(v4), expected2);
            Assert.AreEqual(Vector.Magnitude(v5), expected2);
        }

        [TestMethod]
        public void TestNormalize()
        {
            Vector v1 = new(4, 0, 0);
            Vector expected1 = new(1, 0, 0);
            Assert.AreEqual(Vector.Normalize(v1), expected1);

            Vector v2 = new(1, 2, 3);
            Vector expected2 = new(0.26726f, 0.53452f, 0.80178f); // Divided by sqrt(14)
            Assert.AreEqual(Vector.Normalize(v2), expected2);

            // Magnitude of normalized vector is 1
            Vector norm = Vector.Normalize(v2);
            Assert.IsTrue(Calc.Equals(Vector.Magnitude(norm), 1));
        }

        [TestMethod]
        public void TestDotProduct()
        {
            Vector v1 = new(1, 2, 3);
            Vector v2 = new(2, 3, 4);
            float expected = 20;
            Assert.AreEqual(Vector.DotProduct(v1, v2), expected);
        }

        [TestMethod]
        public void TestCrossProduct()
        {
            Vector v1 = new(1, 2, 3);
            Vector v2 = new(2, 3, 4);
            Vector expected1 = new(-1, 2, -1);
            Vector expected2 = new(1, -2, 1);
            Assert.AreEqual(Vector.CrossProduct(v1, v2), expected1);
            Assert.AreEqual(Vector.CrossProduct(v2, v1), expected2);
        }
    }
}
