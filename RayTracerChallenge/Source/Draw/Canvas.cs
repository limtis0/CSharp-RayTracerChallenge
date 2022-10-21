namespace RT.Source.Draw
{
    public class Canvas
    {
        public readonly int width;
        public readonly int height;
        private readonly Color[,] pixels;

        public Canvas(int width, int height)
        {
            this.width = width;
            this.height = height;

            pixels = new Color[height, width];
            InitPixels();
        }

        private void InitPixels()
        {
            Color black = new();
            for (int h = 0; h < height; h++)
                for (int w = 0; w < width; w++)
                    WritePixel(w, h, black);
        }

        public Color ReadPixel(int width, int height) { return pixels[height, width]; }

        public void WritePixel(int width, int height, Color pixel) { pixels[height, width] = pixel; }

        #region OutputPPM

        // Returns rows, placing new line when length reaches MaxPPMLineSize
        public IEnumerable<string> PPMStrings()
        {
            List<string> line = new();
            Queue<string> queue = new();
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                    FillPixelQueuePPM(queue, w, h);

                while (queue.Count > 0)
                {
                    while (queue.Count > 0 && LineSizePPM(line, queue) <= Picture.MaxPPMLineSize)
                        line.Add(queue.Dequeue());

                    yield return string.Join(" ", line);
                    line.Clear();
                }
            }
        }

        private void FillPixelQueuePPM(Queue<string> queue, int width, int height)
        {
            foreach (string rgbValue in ReadPixel(width, height).ToString().Split())
                queue.Enqueue(rgbValue);
        }

        private static int LineSizePPM(List<string> line, Queue<string> queue)
        {
            return line.Sum(l => l.Length) + line.Count + queue.Peek()!.Length;
        }

        public void ToPPM() { Picture.CanvasToPPM(this); }

        #endregion
    }
}
