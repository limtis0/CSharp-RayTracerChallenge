using RT.Source.Draw;
using RT.Source.Light;
using RT.Source.Vectors;
using static System.Math;

namespace RT.Source.Materials
{
    public struct Material
    {
        public Color color;
        public double ambient;
        public double diffuse;
        public double specular;
        public double shininess;

        public Material()
        {
            color = new Color(1, 1, 1);
            ambient = 0.1;
            diffuse = 0.9;
            specular = 0.9;
            shininess = 200;
        }

        /// <summary>
        /// Used for object coloring and shading.
        /// </summary>
        /// <param name="color">Object's color.</param>
        /// <param name="ambient">Background lighting. Ranges from 0 to 1.</param>
        /// <param name="diffuse">Light reflected from matte surface, depends on angle. Ranges from 0 to 1.</param>
        /// <param name="specular">Reflection of light source itself, small bright spot. Ranges from 0 to 1.</param>
        /// <param name="shininess">Overall shininess. Ranges from 10 (very large highlight) to 200+ (very small highlight).</param>
        public Material(Color color, double ambient = 0.1, double diffuse = 0.9, double specular = 0.9, double shininess = 200)
        {
            AssertBetween(ambient, 0, 1);
            AssertBetween(diffuse, 0, 1);
            AssertBetween(specular, 0, 1);
            AssertBetween(shininess, 10, double.PositiveInfinity);

            this.color = color;
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = shininess;
        }

        private static void AssertBetween(double v, double min, double max)
        {
            if (v < min || v > max)
                throw new ArgumentOutOfRangeException(nameof(v));
        }

        public Color Lighting(PointLight light, Point position, Vector eyeV, Vector normalV, bool inShadow = false)
        {
            Color colAmbient;
            Color colDiffuse;
            Color colSpecular;

            // Combine the surface color with the light's color/intensity
            Color effectiveColor = color * light.intensity;

            // Find the direction to the light source
            Vector lightV = Vector.Normalize(new Vector(light.position - position));

            // Compute the ambient contribution
            colAmbient = effectiveColor * ambient;

            if (inShadow) { return colAmbient; }

            // Compute a cosine of the angle between the
            // lightV and normalV. A negative numbers means the
            // light is one the other side of the surface.
            double lightDotNormal = Vector.DotProduct(lightV, normalV);
            if (lightDotNormal < 0)
            {
                colDiffuse = new(0, 0, 0);
                colSpecular = new(0, 0, 0);
            }
            else
            {
                // Compute the diffuse contribution
                colDiffuse = effectiveColor * diffuse * lightDotNormal;

                // Compute a cosine of the angle between the
                // reflection vector and the eye vector. A negative number means
                // the light reflects away from the eye.
                Vector reflectV = new(-lightV.Reflect(normalV));
                double reflectDotEye = Vector.DotProduct(reflectV, eyeV);
                if (reflectDotEye <= 0)
                {
                    colSpecular = new(0, 0, 0);
                }
                else
                {
                    // Compute the specular contribution
                    double factor = Pow(reflectDotEye, shininess);
                    colSpecular = light.intensity * specular * factor;
                }
            }

            return colAmbient + colDiffuse + colSpecular;
        }
    }
}
