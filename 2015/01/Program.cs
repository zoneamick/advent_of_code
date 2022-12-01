using System;
using AdventReader;

namespace AdventOfCode2015
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();
            string content = reader.Read();

            var chars = content.ToCharArray();

            // char with ( = +1 (up)
            int up = chars.Count(x => x == '(');
            // char with ) = -1 (down)
            int down = chars.Count(x => x != '(');

            int pos = 1;
            int currentLevel = 0;
            for (int i = 0; i < up; i++)
            {
                int val = chars[i] == '(' ? 1 : -1;
                currentLevel += val;
                if (currentLevel <= -1) break;
                pos++;
            }

            // Answers
            Console.WriteLine($"Answer 1: {up - down}");

            Console.WriteLine($"Answer 2: {pos}");
        }
    }
}