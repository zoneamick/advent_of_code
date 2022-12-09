using AdventReader;
using AdventExtensions;

namespace AdventOfCode2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();
            var forest = reader.ReadLines();

            var list = forest.ToTreeList();

            SetHiddenTrees(list);


            // ANSWER 1
            int visibleTrees = list.Where(t => t.HiddenByCount != 4).Count();

            Console.WriteLine($"Answer 1: {visibleTrees} ");

            // ANSWER 2
            int highestScenicScore = list.Max(t => t.ScenicScore);

            Console.WriteLine($"Answer 2: {highestScenicScore}");
        }
        public static void SetHiddenTrees(List<Tree> trees)
        {
            foreach (var tree in trees)
            {
                int scenicScore = 1;
                int maxX = trees.Max(t => t.X);
                int maxY = trees.Max(t => t.Y);

                // Check East (x-1)
                var left = trees.LastOrDefault(t => t.X < tree.X && t.Y == tree.Y && t.Height >= tree.Height);
                if (left != null)
                {
                    tree.HiddenBy.Add(left);
                    scenicScore = scenicScore * (tree.X - left.X);
                }
                else
                {
                    scenicScore = scenicScore * (tree.X - 0);
                }

                // Check West (x+1)
                var right = trees.FirstOrDefault(t => t.X > tree.X && t.Y == tree.Y && t.Height >= tree.Height);
                if (right != null)
                {
                    tree.HiddenBy.Add(right);
                    scenicScore = scenicScore * (right.X - tree.X);
                }
                else
                {
                    scenicScore = scenicScore * (maxX - tree.X);
                }

                // Check North (y-1)
                var north = trees.LastOrDefault(t => t.X == tree.X && t.Y < tree.Y && t.Height >= tree.Height);
                if (north != null)
                {
                    tree.HiddenBy.Add(north);
                    scenicScore = scenicScore * (tree.Y - north.Y);
                }
                else
                {
                    scenicScore = scenicScore * (tree.Y - 0);
                }

                // Check South (y+1)
                var south = trees.FirstOrDefault(t => t.X == tree.X && t.Y > tree.Y && t.Height >= tree.Height);
                if (south != null)
                {
                    tree.HiddenBy.Add(south);
                    scenicScore = scenicScore * (south.Y - tree.Y);
                }
                else
                {
                    scenicScore = scenicScore * (maxY - tree.Y);
                }

                tree.ScenicScore = tree.OnEdge? 0: scenicScore;
            }
        }

    }

    public class Tree
    {
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Position => $"{X},{Y}";
        public List<Tree> HiddenBy { get; set; } = new List<Tree>();
        public int HiddenByCount => HiddenBy.Count;
        public int ScenicScore { get; set; }
        public bool OnEdge { get; set; } = false;
        public Tree(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }
        public override string ToString() => $"{Position} Height:{Height} Score:{ScenicScore}";
    }
    public static class Xtensions
    {
        public static List<Tree> ToTreeList(this List<string> forest)
        {
            List<Tree> treeList = new List<Tree>();

            for (int i = 0; i < forest.Count; i++)
            {
                var treeLines = forest[i].ToCharArray();

                for (int j = 0; j < treeLines.Length; j++)
                {
                    Tree t = new Tree(j, i, treeLines[j].ToInt());

                    // Check if it's on edge
                    if (i == 0 || i == (forest.Count - 1) || j == 0 || j == (treeLines.Length - 1)) t.OnEdge = true;
                    
                    treeList.Add(t);
                }
            }

            return treeList;
        }
        public static bool IsShorterThan(this Tree tree, Tree other) => tree.Height < other.Height;

    }
}