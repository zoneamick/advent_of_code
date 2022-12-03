using AdventReader;

namespace AdventOfCode2022
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Reader reader = new Reader();
            
            List<Rucksack> rucksacks = new List<Rucksack>();
            List<Rucksack> rucksacksV2 = new List<Rucksack>();

            var puzzle = reader.ReadLines();

            // part 1
            foreach(var rucksack in puzzle)
            {
                int charCount = rucksack.Length;
                
                Rucksack rs = new Rucksack();
                rs.Compartment1 = rucksack.Substring(0, charCount/2).ToCharArray().ToList();
                rs.Compartment2 = rucksack.Substring(charCount / 2, charCount/2).ToCharArray().ToList();
                rs.SetSameChars();

                if (rs.SameChars.Any())
                {
                    foreach(var sameChar in rs.SameChars)
                    {
                        rs.Priority += Char.IsLower(sameChar) ? (int)sameChar - 96 : (int)sameChar - 38;
                    }
                }

                rucksacks.Add(rs);
            }

            // part 2
            for(int i = 0; i<puzzle.Count();i++)
            {
                Rucksack rs = new Rucksack(puzzle[i].ToCharArray(), puzzle[i+1].ToCharArray(), puzzle[i+2].ToCharArray());

                rs.SetPriorityV2();

                rucksacksV2.Add(rs);
                i += 2;
            }

            // ANSWER 1
            Console.WriteLine($"Answer 1: {rucksacks.Sum(x => x.Priority)} ");

            // ANSWER 2
            Console.WriteLine($"Answer 2: {rucksacksV2.Sum(x => x.Priority)}");
        }

    }
    public class Rucksack
    {
        public List<char> Compartment1 { get; set; }
        public List<char> Compartment2 { get; set; }
        public List<char> SameChars { get; set; }
        public char[] Group1 { get; set; }
        public char[] Group2 { get; set; }
        public char[] Group3 { get; set; }

        public int Priority { get; set; }
        public Rucksack()
        {

        }
        public Rucksack(char[] group1, char[] group2, char[] group3)
        {
            Group1 = group1;
            Group2 = group2;
            Group3 = group3;
        }
    }
    public static class Xtensions
    {
        public static void SetSameChars(this Rucksack rs)
        {
            List<char> lstChar = new List<char>();

            foreach (char c in rs.Compartment1)
            {
                if (rs.Compartment2.Contains(c)) lstChar.Add(c);
            }

            rs.SameChars = lstChar.Distinct().ToList();
        }
        public static void SetPriorityV2(this Rucksack rs)
        {
            List<char> lstChar = new List<char>();

            foreach(char c in rs.Group1)
            {
                if (rs.Group2.Contains(c) && rs.Group3.Contains(c)) lstChar.Add(c);
            }

            rs.SameChars = lstChar.Distinct().ToList();

            if (rs.SameChars.Any())
            {
                foreach (char c in rs.SameChars)
                {
                    rs.Priority += Char.IsLower(c) ? (int)c - 96 : (int)c - 38;
                }
            }

        }
    }
}