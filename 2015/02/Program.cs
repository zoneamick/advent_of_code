using System;
using AdventReader;

namespace AdventOfCode2015
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalArea = 0;

            Reader reader = new Reader();
            List<string> lines = reader.ReadLines();
            List<Box> boxes = new List<Box>();
            foreach(string line in lines)
            {
                List<int> d = Array.ConvertAll<string, int>(line.Split('x'), int.Parse).ToList();
                d.Sort();
                boxes.Add(new Box(d[0], d[1], d[2]));
            }

            // Answer 1
            Console.WriteLine(boxes.GetWrappingPaperSurfaceArea());

            // Answer 2
            Console.WriteLine(boxes.GetTotalFeetOfRibbon());
        }


    }
    public class Box
    {
        public int L { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public Box(int l, int w, int h)
        {
            L = l;
            W = w;
            H = h;
        }
        public int BoxSurfaceArea() => (2 * L * W) + (2 * W * H) + (2 * H * L);
        public int AreaSmallestSide() => L * W;
        public int FeetOfRibbon() => (L + L + W + W) + (L * W * H);
    }
    public static class Xtensions
    {
        public static int GetWrappingPaperSurfaceArea(this List<Box> boxes)
        {
            int totalOrder = 0;
            foreach(Box box in boxes)
            {
                totalOrder += box.BoxSurfaceArea() + box.AreaSmallestSide();
            }
            return totalOrder;
        }
        public static int GetTotalFeetOfRibbon(this List<Box> boxes)
        {
            int totalFeet = 0;
            foreach (Box box in boxes)
            {
                totalFeet += box.FeetOfRibbon();
            }
            return totalFeet;
        }
    }
}