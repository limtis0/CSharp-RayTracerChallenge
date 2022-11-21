using RT.Source.Draw;
using RT.Source.Vectors;

namespace RT.Source.Light
{
    public class PointLight
    {
        public Point position;
        public Color intensity;

        public PointLight(Point position, Color intensity)
        {
            this.position = position;
            this.intensity = intensity;
        }
    }
}
