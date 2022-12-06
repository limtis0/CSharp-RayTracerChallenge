using RT.Source.Matrices;
using RT.Source.Rays;
using RT.Source.Vectors;
using RT.Source.Draw;
using static System.Math;

namespace RT.Source.World
{
    // TODO: CameraView struct, CameraPosition struct
    public class Camera
    {
        const double HalfPixel = 0.5;
        private readonly World world = World.Instance;

        // Input values
        public readonly int canvasHeight;
        public readonly int canvasWidth;
        public readonly double fov;
        public Matrix transform;

        // Calculated values
        private double halfHeight;
        private double halfWidth;
        public readonly double pixelSize;

        #region Constructors

        public Camera(int canvasHeight, int canvasWidth, double fov, Matrix? transform = null)
        {
            this.canvasHeight = canvasHeight;
            this.canvasWidth = canvasWidth;
            this.fov = fov;
            this.transform = transform ?? Matrix.Identity();

            // Calculated values
            SetHalfCanvas();
            pixelSize = GetPixelSize();
        }

        private void SetHalfCanvas()
        {
            double halfView = Tan(fov / 2);
            double aspectRatio = (double) canvasHeight / canvasWidth;

            if (aspectRatio >= 1)
            {
                halfHeight = halfView / aspectRatio;
                halfWidth = halfView;
            }
            else
            {
                halfHeight = halfView;
                halfWidth = halfView * aspectRatio;
            }
        }

        private double GetPixelSize() => halfWidth * 2 / canvasHeight;

        #endregion

        #region Render

        internal Ray RayForPixel(int row, int col)
        {
            // Offset from the edge of the canvas to the pixel's center
            double hOffset = (row + HalfPixel) * pixelSize;
            double wOffset = (col + HalfPixel) * pixelSize;

            // Untransformed coordinates of the pixel in world space
            // Camera is looking towards -Z
            double worldH = halfHeight - hOffset;
            double worldW = halfWidth - wOffset;

            // Transform using camera matrix
            Matrix invTransform = transform.Inverse();
            
            Point pixel = new(invTransform * new Point(worldW, worldH, -1));
            Point origin = new(invTransform * new Point(0, 0, 0));  // TODO: Static variable for world center
            Vector direction = Vector.Normalize(new(pixel - origin));

            return new Ray(origin, direction);
        }

        public Canvas Render()
        {
            Canvas canvas = new(canvasHeight, canvasWidth);

            for (int row = 0; row < canvasHeight; row++)
            {
                for (int col = 0; col < canvasWidth; col++)
                {
                    Ray ray = RayForPixel(row, col);
                    Color color = world.ColorAt(ray);
                    canvas.SetPixel(row, col, color);
                }
            }

            return canvas;
        }

        // TODO: Loading bar

        #endregion
    }
}
