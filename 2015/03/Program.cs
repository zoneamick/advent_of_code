using System;
using AdventReader;

namespace AdventOfCode2015
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();
            Char[] moves = reader.Read().ToCharArray();

            List<House> houses = new List<House>();
            List<House> housesPartTwo = new List<House>();
            int SantaXP1 = 0;
            int SantaYP1 = 0;
            int SantaX = 0;
            int SantaY = 0;
            int RobotSantaX = 0;
            int RobotSantaY = 0;
            houses.Add(new House(SantaXP1, SantaYP1));
            housesPartTwo.AddHouseOrIncrementGift(SantaX, SantaY);
            housesPartTwo.AddHouseOrIncrementGift(RobotSantaX, RobotSantaY);
            Turn whichTurn = Turn.Santa;

            foreach (char m in moves)
            {
                // PART ONE
                IncrementDecrement(m, ref SantaXP1, ref SantaYP1);
                houses.AddHouseOrIncrementGift(SantaXP1, SantaYP1);


                // PART TWO
                if (whichTurn == Turn.Santa)
                {
                    IncrementDecrement(m, ref SantaX, ref SantaY);
                    housesPartTwo.AddHouseOrIncrementGift(SantaX, SantaY);
                    whichTurn = Turn.RobotSanta;
                }
                else
                {
                    IncrementDecrement(m, ref RobotSantaX, ref RobotSantaY);
                    housesPartTwo.AddHouseOrIncrementGift(RobotSantaX, RobotSantaY);
                    whichTurn = Turn.Santa;
                }

            }

            // Part One
            Console.WriteLine(houses.WithAtLeastAPresent());

            // Part Two
            Console.WriteLine(housesPartTwo.WithAtLeastAPresent());
        }
        public static void IncrementDecrement(char move, ref int x, ref int y)
        {
            switch (move)
            {
                case '^':
                    x++;
                    break;
                case 'v':
                    x--;
                    break;
                case '>':
                    y++;
                    break;
                case '<':
                    y--;
                    break;
            }
        }
    }
    public enum Turn
    {
        Santa,
        RobotSanta
    }
    public class House
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Gifts { get; set; } = 1;
        public House(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public static class Xtensions
    {
        public static int WithAtLeastAPresent(this List<House> houses) => houses.Count();
        public static void AddHouseOrIncrementGift(this List<House> houses, int x, int y)
        {
            if (houses.Any(h => h.X == x && h.Y == y))
            {
                houses.First(h => h.X == x && h.Y == y).Gifts++;
            }
            else
            {
                houses.Add(new House(x, y));
            }
        }
    }
}