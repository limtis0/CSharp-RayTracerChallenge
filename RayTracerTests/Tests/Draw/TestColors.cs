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
            Color c1 = new(0.9f, 0.6f, 0.75f);
            Color c2 = new(0.7f, 0.1f, 0.25f);
            Color expected = new(1.6f, 0.7f, 1.0f);

            Assert.AreEqual(c1 + c2, expected);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            Color c1 = new(0.9f, 0.6f, 0.75f);
            Color c2 = new(0.7f, 0.1f, 0.25f);
            Color expected = new(0.2f, 0.5f, 0.5f);

            Assert.AreEqual(c1 - c2, expected);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            // Multiply by number
            Color c1 = new(0.2f, 0.3f, 0.4f);
            Color expected1 = new(0.4f, 0.6f, 0.8f);

            Assert.AreEqual(c1 * 2, expected1);

            // Multiply by color
            Color c2 = new(1f, 0.2f, 0.4f);
            Color c3 = new(0.9f, 1f, 0.1f);
            Color expected2 = new(0.9f, 0.2f, 0.04f);

            Assert.AreEqual(c2 * c3, expected2);
        }
    }
}
