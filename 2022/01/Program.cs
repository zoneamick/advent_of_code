using System;
using System.Text;
using System.Security.Cryptography;
using AdventReader;

namespace AdventOfCode2015
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Reader reader = new Reader();
            string elvesCalories = reader.Read();

            List<Elf> elves = new List<Elf>();

            List<string> elfCalories = elvesCalories.Split("\r\n\r\n").ToList();
            elfCalories.ForEach(elfSnacks =>
            {
                Elf elf = new Elf();

                elf.FoodCalories.AddRange(Array.ConvertAll<string, int>(elfSnacks.Split("\r\n"), int.Parse));

                elves.Add(elf);
            });

            // ANSWER 1
            Console.WriteLine($"Answer 1: {elves.Select(elf => elf.TotalCalories).Max()}");

            // ANSWER 2
            var top3 = elves.OrderByDescending(elf => elf.TotalCalories).Take(3).ToList();

            Console.WriteLine($"Answer 2: {top3.Sum(x => x.TotalCalories)}");
        }

    }

    public class Elf
    {
        public int TotalCalories => FoodCalories.Sum();
        public List<int> FoodCalories { get; set; } = new List<int>();
        public Elf()
        {

        }
        public override string ToString() => TotalCalories.ToString();
    }

}