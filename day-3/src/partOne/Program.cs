using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace partOne
{
    class Program
    {

        static void Main(string[] args)
        {
            string pattern = "\\d+";
            string[] lines = File.ReadAllLines(@"./input.txt");
            List<Rect> rectangles = new List<Rect>();
            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();

            foreach (var line in lines)
            {
                var newRect = LineToRect(line, pattern);
                xCoords.Add(newRect.posX);
                yCoords.Add(newRect.posY);
                
                rectangles.Add(newRect);
            }

            xCoords.Sort();
            Sweep(xCoords.ToArray(), rectangles);
        }

        static Rect LineToRect(string line, string pattern)
        {
            var numbers = Regex.Matches(line, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));

            return new Rect
            {
                posX = Int32.Parse(numbers[1].Value),
                posY = Int32.Parse(numbers[2].Value),
                length = Int32.Parse(numbers[3].Value),
                width = Int32.Parse(numbers[4].Value)
            };
        }

        static void Sweep(int[] xCoords, List<Rect> rectangles)
        {
            List<int> yCoords = new List<int>();
            for (int i = 0; i < xCoords.Length; i++)
            {
                var x = xCoords[i];
                var rectanglesStartOnX = rectangles.Where(r => r.posX == x);
                var rectanglesEndOnX = rectangles.Where(r => r.posX + (r.length -1) == x);

                foreach(var rect in rectanglesStartOnX)
                {
                    yCoords.Add(rect.posY);
                }

                foreach(var rect in rectanglesEndOnX)
                {
                    yCoords.Remove(rect.posY);
                }
            }
        }
    }

    struct Rect
    {
        public int posX { get; set; }
        public int posY { get; set; }
        public int length { get; set; }
        public int width { get; set; }
    }
}