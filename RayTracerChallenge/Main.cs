using RT.Source;
using RT.Source.Draw;
using RT.Source.Vectors;

namespace RT
{
    class RayTracerChallenge
    {
        private static void Main()
        {
            // Generates a basic dotted circle
            const int size = 1079;
            const int center = size / 2 + 1;
            const int radius = 405;

            Canvas canvas = new(size, size);

            Source.Vectors.Tuple p = new Point(0, 0, 1);
            Color white = new(1, 1, 1);

            // W, H
            for (int i = 0; i < 90; i++)
            {
                canvas.SetPixel(center - (int)(radius * p.z), center - (int)(radius * p.x), white);
                p = p.RotateY(Calc.PI / 45);
            }

            canvas.ToPPM();
        }
    }
}
