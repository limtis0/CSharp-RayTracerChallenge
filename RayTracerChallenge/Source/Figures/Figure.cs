using RT.Source.Matrices;
using RT.Source.Vectors;

namespace RT.Source.Figures
{
    public abstract class Figure
    {
        // public static readonly int Id = 0;
        public readonly Point origin = new(0, 0, 0);
        public Matrix transform = Matrix.Identity();

        public abstract Vector NormalAt(Point p);
    }
}
