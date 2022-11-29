using RT.Source.Matrices;
using RT.Source.Vectors;
using RT.Source.Materials;

namespace RT.Source.Figures
{
    public abstract class Figure
    {
        private static int globalId = 0;
        public readonly int id = globalId++;

        public readonly Point origin = new(0, 0, 0);

        public Matrix transform = Matrix.Identity();

        public Material material;

        public abstract Vector NormalAt(Point p);
    }
}
