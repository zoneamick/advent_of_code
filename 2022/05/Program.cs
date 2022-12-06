using AdventReader;
using AdventExtensions;

namespace AdventOfCode2022
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var puzzleLines = new Reader().ReadLines();
            Stack stack = puzzleLines.ReadStack();
            Stack stackA2 = puzzleLines.ReadStack();
            stack.ReverseCrateYPositions();
            stackA2.ReverseCrateYPositions();
            stack.MoveCrates();
            stackA2.MoveCrates(true);

            // Answer 1
            Console.WriteLine($"Answer 1: {stack.GetMessage()}");

            // Answer 1
            Console.WriteLine($"Answer 2: {stackA2.GetMessage()}");
        }

    }

    public class Crate
    {
        public char CrateID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Crate(char crateID)
        {
            CrateID = crateID;
        }
        public override string ToString() => CrateID.ToString();
    }
    public class Stack
    {
        public List<Crate> Crates { get; set; } = new List<Crate>();
        public List<Move> Moves { get; set; } = new List<Move>();
        public Stack()
        {

        }

    }
    public class Move
    {
        public int Quantity { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public Move()
        {

        }
        public override string ToString()
        {
            return $"move {Quantity} from {From} to {To}";
        }
    }
    public static class Xtensions
    {
        public static Stack ReadStack(this List<string> text)
        {
            Stack stack = new Stack();
            int yId = 0;

            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].Contains('['))
                {
                    yId++;
                    stack.Crates.AddRange(text[i].ToCrateList(yId));
                }

                if (text[i].StartsWith("move")) stack.Moves.Add(text[i].ToCraneMovement());

            }

            return stack;
        }
        public static List<Crate> ToCrateList(this string text, int Y)
        {
            // x * 4 + 1 for X position
            int[] j = new int[] { 1, 5, 9, 13, 17, 21, 25, 29, 33, 37 }; // char position

            List<Crate> CrateList = new List<Crate>();

            var chars = text.ToCharArray();
            for (int i=0; i<chars.Length; i++)
            {
                if (!j.Contains(i)) continue;
                if (chars[i] == ' ') continue;

                Crate crate = new Crate(chars[i]);
                crate.X = ((i-1) /4)+1; // Reverse Formula (x-1)/4 +1 for the correct ID
                crate.Y = Y;

                CrateList.Add(crate);
            }

            return CrateList;
        }
        public static void ReverseCrateYPositions(this Stack stack)
        {
            int maxY = stack.Crates.Max(x => x.Y);
            foreach(var crate in stack.Crates)
            {
                crate.Y = maxY-crate.Y+1;
            }
        }
        public static Move ToCraneMovement(this string text)
        {
            Move move = new Move();

            text = text.Replace("move ", String.Empty);
            text = text.Replace(" from ", "~");
            text = text.Replace(" to ", "~");
            var pos = text.Split('~');
            move.Quantity = pos[0].ToInt();
            move.From = pos[1].ToInt();
            move.To = pos[2].ToInt();

            return move;
        }
        public static void MoveCrates(this Stack stack, bool isCrateMover9001 = false)
        {
            foreach(var move in stack.Moves)
            {
                var crates = stack.Crates.Where(c => c.X == move.From).OrderByDescending(c => c.Y).Take(move.Quantity);

                int destinationStartingY = 0;
                if (stack.Crates.Where(c => c.X == move.To).Any())
                {
                    destinationStartingY = stack.Crates.Where(c => c.X == move.To).OrderByDescending(c => c.Y).Max(c => c.Y);
                }

                var crateSort = crates;
                if (isCrateMover9001) crateSort = crates.OrderBy(c => c.Y);
                foreach(var crate in crateSort)
                {
                    destinationStartingY++;
                    crate.X = move.To;
                    crate.Y = destinationStartingY;
                }
            }
        }
        public static string GetMessage(this Stack stack)
        {
            string message = "";

            int columns = stack.Crates.Max(c => c.X);
            for (int i = 1; i <= columns; i++)
            {
                if (stack.Crates.Where(c => c.X == i).Any())
                {
                    var crate = stack.Crates.Where(c => c.X == i).OrderBy(c => c.Y).Last();
                    message += crate != null ? crate.ToString() : "";
                }
                else message += " ";
            }
            return message;
        }
    }
}