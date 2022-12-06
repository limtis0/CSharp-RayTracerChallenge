using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Draw;

namespace RayTracerTests
{
    [TestClass]
    public class TestColors
    {
        [TestMethod]
        public void TestAddition()
        {
            Color c1 = new(0.9, 0.6, 0.75);
            Color c2 = new(0.7, 0.1, 0.25);
            Color expected = new(1.6, 0.7, 1.0);

            Assert.AreEqual(c1 + c2, expected);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            Color c1 = new(0.9, 0.6, 0.75);
            Color c2 = new(0.7, 0.1, 0.25);
            Color expected = new(0.2, 0.5, 0.5);

            Assert.AreEqual(c1 - c2, expected);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            // Multiply by number
            Color c1 = new(0.2, 0.3, 0.4);
            Color expected1 = new(0.4, 0.6, 0.8);

            Assert.AreEqual(c1 * 2, expected1);


            // Multiply by color
            Color c2 = new(1, 0.2, 0.4);
            Color c3 = new(0.9, 1, 0.1);
            Color expected2 = new(0.9, 0.2, 0.04);

            Assert.AreEqual(c2 * c3, expected2);
        }
    }
}
