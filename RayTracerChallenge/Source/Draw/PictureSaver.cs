namespace RT.Source.Draw
{
    internal class PictureSaver
    {
        public const int MaxPPMLineSize = 70;
        private const string PicturesPath = @"RayTracerChallenge\Pictures\";

        private static readonly string documentsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string picturesDir = Path.Combine(documentsDir, PicturesPath);

        // Returns unique filename
        private static string NewFilePath(string extension) =>
            Path.Combine(picturesDir, $"{DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks:x}.{extension}");

        private static void WritePPMHeader(StreamWriter sw, Canvas canvas)
        {
            sw.WriteLine("P3");
            sw.WriteLine($"{canvas.width} {canvas.height}");
            sw.WriteLine("255");
        }

        public static void CanvasToPPM(Canvas canvas)
        {
            Directory.CreateDirectory(picturesDir);

            using StreamWriter sw = File.CreateText(NewFilePath("ppm"));
            
            WritePPMHeader(sw, canvas);
            foreach (string s in canvas.PPMStrings())
                sw.WriteLine(s);

            sw.Close();
        }
    }
}
