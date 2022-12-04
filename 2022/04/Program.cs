using AdventReader;

namespace AdventOfCode2022
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var lines = new Reader().ReadLines();
            List<SectionAssignement> assignments = new List<SectionAssignement>();
            foreach(var line in lines)
            {
                SectionAssignement assignement = new SectionAssignement(line);
                assignments.Add(assignement);
            }

            // Answer 1
            Console.WriteLine($"Answer1 : {assignments.Count(x => x.FullyContainsTheOther)}");

            // Answer 2
            Console.WriteLine($"Answer1 : {assignments.Count(x => x.IsOverlapping)}");
        }

    }
    public class SectionAssignement
    {
        public SectionAssignement(string assignementPair)
        {
            var sections = assignementPair.Split(',');
            Section1 = sections[0].Split('-');
            Section2 = sections[1].Split('-');

            S1 = Enumerable.Range(startS1, stopS1-startS1+1);
            S2 = Enumerable.Range(startS2, stopS2-startS2+1);

            IsOverlapping = CheckIsOverlapping();
        }

        private bool CheckIsOverlapping()
        {
            foreach (var num in S1) if (S2.Contains(num)) return true;
            foreach (var num in S2) if (S1.Contains(num)) return true;
            return false;
        }

        public IEnumerable<string> Section1 { get; set; }
        private int startS1 => Section1.First().ToInt();
        private int stopS1 => Section1.Last().ToInt();
        private IEnumerable<int> S1 { get; set; }
        public IEnumerable<string> Section2 { get; set; }
        private int startS2 => Section2.First().ToInt();
        private int stopS2 => Section2.Last().ToInt();
        private IEnumerable<int> S2 { get; set; }
        public bool FullyContainsTheOther => S1.All(x => S2.Any(y => y == x)) || S2.All(x => S1.Any(y => y == x));
        public bool IsOverlapping;
    }
    public static class Xtensions
    {
        public static int ToInt(this string strInt)
        {
            return Convert.ToInt32(strInt);
        }
    }
}