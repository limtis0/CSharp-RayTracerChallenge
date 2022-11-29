namespace RT.Source.Draw
{
    internal static class PictureSaver
    {
        public const int MaxPPMLineSize = 70;
        private const string PicturesPath = @"RayTracerChallenge\Pictures\";
        private const string FileExtension = ".ppm";

        private static readonly string documentsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string picturesDir = Path.Combine(documentsDir, PicturesPath);

        private static string ReverseString(string s)
        {
            char[] chararr = s.ToCharArray();
            Array.Reverse(chararr);
            return new string(chararr);
        }

        // Returns unique filename, reversed for ease of searching
        private static string NewFilePath(string filename)
        {
            // Generate filename if null
            if (string.IsNullOrEmpty(filename))
                filename = ReverseString((DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x"));

            // Add extension if needed
            if (!filename.EndsWith(FileExtension))
                filename = $"{filename}{FileExtension}";

            return Path.Combine(picturesDir, filename);
        }

        private static void WritePPMHeader(StreamWriter sw, Canvas canvas)
        {
            sw.WriteLine("P3");
            sw.WriteLine($"{canvas.width} {canvas.height}");
            sw.WriteLine("255");
        }

        public static void CanvasToPPM(Canvas canvas, string filename = "")
        {
            Directory.CreateDirectory(picturesDir);

            filename = NewFilePath(filename);

            using StreamWriter sw = File.CreateText(filename);

            WritePPMHeader(sw, canvas);
            foreach (string s in canvas.PPMStrings())
                sw.WriteLine(s);

            sw.Close();

            Console.WriteLine($"Successfully saved a render to {filename}");
        }
    }
}
