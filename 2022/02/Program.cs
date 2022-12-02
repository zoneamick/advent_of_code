using System;
using System.Text;
using System.Security.Cryptography;
using AdventReader;

namespace AdventOfCode2022
{
    public class Program
    {

        public int HisScore = 0;
        public int MyScore = 0;

        static void Main(string[] args)
        {

            Reader reader = new Reader();
            var gamePlayed = reader.ReadLines();
            List<Game> gameList = new List<Game>();

            foreach (var gameSummary in gamePlayed)
            {
                gameList.Add(new Game(gameSummary));
            }


            // ANSWER 1
            Console.WriteLine($"Answer 1: {gameList.Select(g => g.Score).Sum()}");

            // ANSWER 2
            Console.WriteLine($"Answer 2: {gameList.Select(g => g.ScorePart2).Sum()}");
        }

        public class Game
        {
            public string Summary { get; } // ex: "A Y"
            RPS HisMove { get; }
            RPS YourMove { get; }
            RPS YouExpectedMove { get; }
            GameResult Result { get; set; }
            GameResult ExpectedEnd { get; set; }
            public int Score { get; private set; }
            public int ScorePart2 { get; private set; }

            public Game(string summary)
            {
                Summary = summary;
                HisMove = Summary.Split(' ')[0].ToRockPaperScissorMove();
                YourMove = Summary.Split(' ')[1].ToRockPaperScissorMove();

                GetGameResult();
                GetExpectedEnd();
                YouExpectedMove = ExpectedEnd.ToRockPaperScissorMove(HisMove);

                Score = (int)Result + (int)YourMove;
                ScorePart2 = (int)ExpectedEnd + (int)YouExpectedMove;
            }
            private void GetGameResult()
            {
                if (HisMove.Equals(YourMove)) Result = GameResult.Draw;
                else if ((HisMove == RPS.Rock && YourMove == RPS.Paper) ||
                    (HisMove == RPS.Paper && YourMove == RPS.Scissor) ||
                    (HisMove == RPS.Scissor && YourMove == RPS.Rock)) Result = GameResult.Win;
                else Result = GameResult.Loss;
            }
            private void GetExpectedEnd()
            {
                if (YourMove == RPS.Rock) ExpectedEnd = GameResult.Loss; // X
                else if (YourMove == RPS.Paper) ExpectedEnd = GameResult.Draw; // Y
                else ExpectedEnd = GameResult.Win; // Z
            }
            public override string ToString() => $"'{Summary}' {Result}, hismove: {HisMove}, yourmove: {YourMove}, final score: {Score}";
        }


    }
    public enum RPS
    {
        Rock = 1,
        Paper = 2,
        Scissor = 3
    }
    public enum GameResult
    {
        Loss = 0,
        Draw = 3,
        Win = 6
    }
    public static class XtensionsMethods
    {
        public static RPS ToRockPaperScissorMove(this string move)
        {
            if (move == "A" || move == "X") return RPS.Rock;
            else if (move == "B" || move == "Y") return RPS.Paper;
            else return RPS.Scissor;
        }
        public static RPS ToRockPaperScissorMove(this GameResult result, RPS hisMove)
        {
            if (result == GameResult.Win)
            {
                if (hisMove == RPS.Rock) return RPS.Paper;
                else if (hisMove == RPS.Paper) return RPS.Scissor;
                else return RPS.Rock;
            }
            else if (result == GameResult.Loss)
            {
                if (hisMove == RPS.Rock) return RPS.Scissor;
                else if (hisMove == RPS.Paper) return RPS.Rock;
                else return RPS.Paper;
            }
            else return hisMove;
        }
    }

}