using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Draw;

namespace RayTracerTests
{
    [TestClass]
    public class TestCanvas
    {
        [TestMethod]
        public void TestCreation()
        {
            // Check height/width
            int h = 20;
            int w = 10;
            Canvas canvas = new(h, w);

            Assert.AreEqual(h, canvas.height);
            Assert.AreEqual(w, canvas.width);


            // Every pixel is black by default
            bool isBlack = true;
            Color black = new(0, 0, 0);

            for (int row = 0; row < canvas.height && isBlack; row++)
                for (int col = 0; col < canvas.width && isBlack; col++)
                    if (canvas.GetPixel(row, col) != black)
                        isBlack = false;

            Assert.IsTrue(isBlack);
        }

        [TestMethod]
        public void TestWrite()
        {
            Canvas canvas = new(10, 10);
            Color red = new(1, 0, 0);

            canvas.SetPixel(1, 1, red);

            Assert.AreEqual(canvas.GetPixel(1, 1), red); // Changed needed
            Assert.AreNotEqual(canvas.GetPixel(2, 3), red); // Not changed others
        }

        [TestMethod]
        public void TestPPMLines()
        {
            Canvas canvas = new(2, 10);
            Color color = new(1, 0.8, 0.6);
            for (int row = 0; row < canvas.height; row++)
                for (int col = 0; col < canvas.width; col++)
                    canvas.SetPixel(row, col, color);

            string text = string.Join("\n", canvas.PPMStrings());
            string expected = "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n" +
                              "153 255 204 153 255 204 153 255 204 153 255 204 153\n" +
                              "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n" +
                              "153 255 204 153 255 204 153 255 204 153 255 204 153";

            Assert.AreEqual(text, expected);
        }
    }
}