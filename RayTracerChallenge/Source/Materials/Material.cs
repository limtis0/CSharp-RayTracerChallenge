using RT.Source.Draw;
using RT.Source.Light;
using RT.Source.Vectors;
using static System.MathF;

namespace RT.Source.Materials
{
    public struct Material
    {
        public Color color;
        public float ambient;
        public float diffuse;
        public float specular;
        public float shininess;

        public Material()
        {
            color = new Color(1, 1, 1);
            ambient = 0.1f;
            diffuse = 0.9f;
            specular = 0.9f;
            shininess = 200f;
        }

        /// <summary>
        /// Used for object coloring and shading.
        /// </summary>
        /// <param name="color">Object's color.</param>
        /// <param name="ambient">Background lighting. Ranges from 0f to 1f.</param>
        /// <param name="diffuse">Light reflected from matte surface, depends on angle. Ranges from 0f to 1f.</param>
        /// <param name="specular">Reflection of light source itself, small bright spot. Ranges from 0f to 1f.</param>
        /// <param name="shininess">Overall shininess. Ranges from 10f (very large highlight) to 200f+ (very small highlight).</param>
        public Material(Color color, float ambient, float diffuse, float specular, float shininess)
        {
            AssertBetween(ambient, 0f, 1f);
            AssertBetween(diffuse, 0f, 1f);
            AssertBetween(specular, 0f, 1f);
            AssertBetween(shininess, 10f, float.PositiveInfinity);

            this.color = color;
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = shininess;
        }

        private static void AssertBetween(float v, float min, float max)
        {
            if (v < min || v > max)
                throw new ArgumentOutOfRangeException(nameof(v));
        }

        public Color Lighting(PointLight light, Point position, Vector eyeV, Vector normalV)
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

            // Compute a cosine of the angle between the
            // lightV and normalV. A negative numbers means the
            // light is one the other side of the surface.
            float lightDotNormal = Vector.DotProduct(lightV, normalV);
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
                float reflectDotEye = Vector.DotProduct(reflectV, eyeV);
                if (reflectDotEye <= 0)
                {
                    colSpecular = new(0, 0, 0);
                }
                else
                {
                    // Compute the specular contribution
                    float factor = Pow(reflectDotEye, shininess);
                    colSpecular = light.intensity * specular * factor;
                }
            }

            return colAmbient + colDiffuse + colSpecular;
        }
    }
}
