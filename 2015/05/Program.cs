using System;
using AdventReader;

namespace AdventOfCode2015
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var lines = new Reader().ReadLines();

            int niceStrings = 0;
            int niceNewRules = 0;

            foreach(var line in lines)
            {
                if (line.ToCharArray().IsNaughty()) continue;
                niceStrings++;
            }
            foreach (var line in lines)
            {
                if (line.ToCharArray().IsNaughtyNewRule()) continue;
                niceNewRules++;
            }

            // PART ONE
            Console.WriteLine($"Answer 1: {niceStrings}");

            // PART TWO
            Console.WriteLine();
        }

    }


    public static class Xtensions
    {
        public static bool IsNaughtyNewRule(this char[] chars)
        {

            return false;
        }
        public static bool IsNaughty(this char[] chars)
        {
            if (chars.VowelsCount() < 3) return true;
            if (!chars.ContainsDoubleLetter()) return true;
            if (chars.ContainsSpecificLetters()) return true;
            return false;
        }
        public static int VowelsCount(this char[] chars)
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            int count = 0;
            foreach(char c in chars)
            {
                if (vowels.Contains(c)) count++;
            }
            return count;
        }
        public static bool ContainsDoubleLetter(this char[] chars)
        {
            for (int i = 0;i < chars.Length;i++)
            {
                if (i + 1 < chars.Length && chars[i] == chars[i+1]) return true;
            }

            return false;
        }
        public static bool ContainsSpecificLetters(this char[] chars)
        {
            List<char[]> naughty = new List<char[]>()
            {
                new char[] {'a', 'b'},
                new char[] {'c', 'd'},
                new char[] {'p', 'q'},
                new char[] {'x', 'y'}
            };

            foreach (var cArr in naughty)
            {
                for (int i = 0;i < chars.Length;i++)
                {
                    if (i + 1 < chars.Length && chars[i] == cArr[0] && chars[i+1] == cArr[1]) return true;
                }
            }

            return false;
        }

        public static bool HasAPairOverlapping(this char[] chars)
        {
            for (int i=0;i<chars.Length;i++)
            {
                if (i + 1 >= chars.Length) break;

            }
            return false;
        }
    }
}