namespace RT.Source.Draw
{
    public class Canvas
    {
        public readonly int height;
        public readonly int width;
        private readonly Color[,] pixels;

        public Canvas(int height, int width)
        {
            this.height = height;
            this.width = width;

            pixels = new Color[height, width];
            InitPixels();
        }

        private void InitPixels()
        {
            Color black = new();
            for (int h = 0; h < height; h++)
                for (int w = 0; w < width; w++)
                    SetPixel(h, w, black);
        }

        public Color GetPixel(int row, int col) => pixels[row, col];

        public void SetPixel(int row, int col, Color pixel) => pixels[row, col] = pixel;

        #region OutputPPM

        // Returns rows, placing new line when length reaches MaxPPMLineSize
        internal IEnumerable<string> PPMStrings()
        {
            List<string> line = new();
            Queue<string> queue = new();
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                    FillPixelQueuePPM(queue, row, col);

                while (queue.Count > 0)
                {
                    while (queue.Count > 0 && LineSizePPM(line, queue) <= PictureSaver.MaxPPMLineSize)
                        line.Add(queue.Dequeue());

                    yield return string.Join(" ", line);
                    line.Clear();
                }
            }
        }

        private void FillPixelQueuePPM(Queue<string> queue, int height, int width)
        {
            foreach (string rgbValue in GetPixel(height, width).ToString().Split())
                queue.Enqueue(rgbValue);
        }

        private static int LineSizePPM(List<string> line, Queue<string> queue)
        {
            return line.Sum(l => l.Length) + line.Count + queue.Peek()!.Length;
        }

        public void ToPPM() => PictureSaver.CanvasToPPM(this);

        #endregion
    }
}
