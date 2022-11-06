using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Draw;
using System;

namespace RayTracerTests
{
    [TestClass]
    public class TestCanvas
    {
        [TestMethod]
        public void TestCreation()
        {
            // Check width/height
            int w = 10;
            int h = 20;
            Canvas canvas = new(w, h);
            
            Assert.AreEqual(w, canvas.width);
            Assert.AreEqual(h, canvas.height);

            // Every pixel is black by default
            bool isBlack = true;
            Color black = new(0, 0, 0);

            for (int i = 0; i < canvas.width && isBlack; i++)
                for (int j = 0; j < canvas.height && isBlack; j++)
                    if (canvas.GetPixel(i, j) != black)
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
            Canvas canvas = new(10, 2);
            Color color = new(1f, 0.8f, 0.6f);
            for (int h = 0; h < canvas.height; h++)
                for (int w = 0; w < canvas.width; w++)
                    canvas.SetPixel(w, h, color);

            string text = string.Join("\n", canvas.PPMStrings());
            string expected = "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n" +
                              "153 255 204 153 255 204 153 255 204 153 255 204 153\n" +
                              "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n" +
                              "153 255 204 153 255 204 153 255 204 153 255 204 153";
            
            Assert.AreEqual(text, expected);
        }
    }
}