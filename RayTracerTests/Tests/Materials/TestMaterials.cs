using Microsoft.VisualStudio.TestTools.UnitTesting;
using RT.Source.Draw;
using RT.Source.Light;
using RT.Source.Materials;
using RT.Source.Vectors;
using static System.Math;

namespace RayTracerTests
{
    [TestClass]
    public class TestMaterials
    {
        [TestMethod]
        public void TestCreation()
        {
            // Default material
            Material m = new();

            Assert.AreEqual(m.color, new Color(1, 1, 1));
            Assert.AreEqual(m.ambient, 0.1);
            Assert.AreEqual(m.diffuse, 0.9);
            Assert.AreEqual(m.specular, 0.9);
            Assert.AreEqual(m.shininess, 200);
        }

        [TestMethod]
        public void TestLighting()
        {
            // Setup
            Material m = new();
            Point position = new(0, 0, 0);
            double r2d2 = Sqrt(2) / 2;


            // Lighting with eye between the light and the surface
            Vector eyeV1 = new(0, 0, -1);
            Vector normalV1 = new(0, 0, -1);
            PointLight light1 = new(new Point(0, 0, -10), new Color(1, 1, 1));
            Color result1 = m.Lighting(light1, position, eyeV1, normalV1);

            Assert.AreEqual(result1, new Color(1.9, 1.9, 1.9));


            // Same as previous, but with 45deg offset
            Vector eyeV2 = new(0, r2d2, -r2d2);
            Color result2 = m.Lighting(light1, position, eyeV2, normalV1);

            Assert.AreEqual(result2, new Color(1, 1, 1));


            // Lighting with eye opposite surface, 45deg offset
            PointLight light3 = new(new Point(0, 10, -10), new Color(1, 1, 1));
            Color result3 = m.Lighting(light3, position, eyeV1, normalV1);

            Assert.AreEqual(result3, new Color(0.7364, 0.7364, 0.7364));


            // Lighting with eye in the path of the reflection vector
            Vector eyeV4 = new(0, -r2d2, -r2d2);
            Color result4 = m.Lighting(light3, position, eyeV4, normalV1);

            Assert.AreEqual(result4, new Color(1.63639, 1.63639, 1.63639));


            // Lighting behing surface
            PointLight light5 = new(new Point(0, 0, 10), new Color(1, 1, 1));
            Color result5 = m.Lighting(light5, position, eyeV1, normalV1);

            Assert.AreEqual(result5, new Color(0.1, 0.1, 0.1));
        }

        [TestMethod]
        public void TestLightingInShadow()
        {
            Material m = new();
            Point position = new(0, 0, 0);

            Vector eyeV = new(0, 0, -1);
            Vector normalV = new(0, 0, -1);
            PointLight light = new(new Point(0, 0, -10), new Color(1, 1, 1));
            bool inShadow = true;

            Color result = m.Lighting(light, position, eyeV, normalV, inShadow);
            Assert.AreEqual(result, new Color(0.1, 0.1, 0.1));
        }
    }
}
