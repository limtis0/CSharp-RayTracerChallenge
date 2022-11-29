using RT.Source.Matrices;
using RT.Source.Rays;
using RT.Source.Vectors;
using RT.Source.Draw;
using static System.MathF;

namespace RT.Source.World
{
    // TODO: CameraView struct, CameraPosition struct
    public class Camera
    {
        const float HalfPixel = 0.5f;
        private readonly World world = World.Instance;

        // Input values
        public readonly int canvasHeight;
        public readonly int canvasWidth;
        public readonly float fov;
        public Matrix transform;

        // Calculated values
        private float halfHeight;
        private float halfWidth;
        public readonly float pixelSize;

        #region Constructors

        public Camera(int canvasHeight, int canvasWidth, float fov, Matrix? transform = null)
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
            float halfView = Tan(fov / 2);
            float aspectRatio = (float) canvasHeight / canvasWidth;

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

        private float GetPixelSize() => halfWidth * 2 / canvasHeight;

        #endregion

        #region Render

        internal Ray RayForPixel(int row, int col)
        {
            // Offset from the edge of the canvas to the pixel's center
            float hOffset = (row + HalfPixel) * pixelSize;
            float wOffset = (col + HalfPixel) * pixelSize;

            // Untransformed coordinates of the pixel in world space
            // Camera is looking towards -Z
            float worldH = halfHeight - hOffset;
            float worldW = halfWidth - wOffset;

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
