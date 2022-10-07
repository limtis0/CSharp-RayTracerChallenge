﻿namespace RT.Source.Draw
{
    internal class Picture
    {
        public const int MaxPPMLineSize = 70;

        private static readonly string documentsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string picturesDir = Path.Combine(documentsDir, @"RayTracerProject\Pictures\");

        private static string NewFilePath(string extension)
        {
            string timestamp = DateTime.Now.ToFileTime().ToString();
            return Path.Combine(picturesDir, $"{timestamp}.{extension}");
        }

        private static void WritePPMHeader(StreamWriter sw, Canvas canvas)
        {
            sw.WriteLine("P3");
            sw.WriteLine($"{canvas.width} {canvas.height}");
            sw.WriteLine("255");
        }

        public static void CanvasToPPM(Canvas canvas)
        {
            using StreamWriter sw = File.CreateText(NewFilePath("ppm"));
            
            WritePPMHeader(sw, canvas);
            foreach (string s in canvas.PPMStrings())
                sw.WriteLine(s);

            sw.Close();
        }
    }
}
