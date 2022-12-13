using AdventReader;
using AdventExtensions;

namespace AdventOfCode2022
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var moveList = new Reader().ReadLines();

            Grid grid1 = new Grid(2);
            grid1.RetrieveMoves(moveList);
            grid1.ExecuteMovements();

            // Answer 1
            Console.WriteLine($"Answer 1: {grid1.Positions.Last().Log.Distinct().Count()}");

            Grid grid2 = new Grid(10);
            grid2.RetrieveMoves(moveList);
            grid2.ExecuteMovements();

            // Answer 2
            Console.WriteLine($"Answer 2: {grid2.Positions.Last().Log.Distinct().Count()}");
        }



    }

    public class Grid
    {
        public List<string> HeadPositionLog { get; set; } = new List<string>();
        public List<string> TailPositionLog { get; set; } = new List<string>();
        public List<Move> Moves { get; set; } = new List<Move>();
        public List<Position> Positions { get; set; } = new List<Position>();
        public Move CurrentMovement { get; private set; }
        public Grid(int numberOfKnots = 2)
        {
            if (numberOfKnots < 2) throw new Exception("Number of knots do not met the minimum requirements of 2.");

            for (int i = 0; i < numberOfKnots; i++)
            {
                Positions.Add(new Position(0,0));
                if (i != (numberOfKnots-1))
                {
                    Positions[i].PositionChanged += Position_PositionChanged;
                }
            }

        }

        private void Position_PositionChanged(object? sender, Position.PositionChangedArgs e)
        {
            Position? p = sender as Position;

            if (p != null)
            {
                int index = Positions.IndexOf(p);
                if (index != Positions.Count - 1)
                {
                    Position nextKnot = Positions[index + 1];
                    NextKnotPosition nkp = new NextKnotPosition(Positions[index], nextKnot, CurrentMovement);

                    if (nkp.NextPosition != null) nextKnot.SetNewPosition(nkp.NextPosition);

                }
            }

        }

        public void RetrieveMoves(List<string> moves)
        {
            foreach(var move in moves) Moves.Add(new Move(move));
        }
        
        public void ExecuteMovements()
        {
            if (!Moves.Any()) throw new Exception("No movement found.");

            foreach(var move in Moves)
            {
                ExecuteMove(move);
            }
        }
        public void ExecuteMove(Move move)
        {
            CurrentMovement = move;

            if (move.X != 0)
            {
                int x = move.X;
                while (x != 0)
                {
                    if (x > 0)
                    {
                        Positions.First().X++;
                        x--;
                    }
                    if (x < 0)
                    {
                        Positions.First().X--;
                        x++;
                    }
                }
            }
            if (move.Y != 0)
            {
                int y = move.Y;
                while (y != 0)
                {
                    if (y > 0)
                    {
                        Positions.First().Y++;
                        y--;
                    }
                    if (y < 0)
                    {
                        Positions.First().Y--;
                        y++;
                    }
                }
            }
        }
    }
    public class NextKnotPosition
    {
        public bool SameRow { get; private set; }
        public bool SameColumn { get; private set; }
        public bool IsTouching { get; private set; }
        public Position? NextPosition { get; private set; }
        bool x_AGtB { get; set; }
        bool y_AGtB { get; set; }
        public NextKnotPosition(Position a, Position b, Move currentMove)
        {
            SameRow = a.Y == b.Y ? true : false;
            SameColumn = a.X == b.X ? true : false;
            x_AGtB = a.X > b.X;
            y_AGtB = a.Y > b.Y;
            int resultX = x_AGtB ? a.X - b.X : b.X - a.X;
            int resultY = y_AGtB ? a.Y - b.Y : b.Y - a.Y;
            IsTouching = resultX < 2 && resultY < 2;


            if (!IsTouching)
            {
                NextPosition = new Position(b.X,b.Y);

                if (SameRow && resultX > 1) NextPosition.X += x_AGtB ? 1 : -1;
                else if (SameColumn && resultY > 1) NextPosition.Y += y_AGtB ? 1 : -1;
                else
                {
                    NextPosition.X += x_AGtB ? 1 : -1;
                    NextPosition.Y += y_AGtB ? 1 : -1;
                }

            }



        }
    }
    public class Position
    {
        public Guid Id = Guid.NewGuid();
        private int x;
        public int X {
            get => x; 
            set
            {
                PositionChangedArgs args = new();
                args.LastX = x;
                x = value;
                args.NewX = x;
                OnPositionChanged(args);
            }
        }
        private int y;
        public int Y
        {
            get => y;
            set
            {
                PositionChangedArgs args = new();
                args.LastY = y;
                y = value;
                args.NewY = y;
                OnPositionChanged(args);
            }
        }
        public event EventHandler<PositionChangedArgs> PositionChanged;
        public List<string> Log = new List<string>();
        public Position(int x, int y)
        {
            SetNewPosition(x, y);
        }
        public Position(string strPos)
        {
            if (!strPos.Contains(',')) throw new Exception("Invalid string, doesn't contains a ','.");
            var s = strPos.Split(',');
            SetNewPosition(s[0].ToInt(), s[1].ToInt());
        }
        public virtual void OnPositionChanged(PositionChangedArgs e)
        {
            PositionChanged?.Invoke(this, e);
            Log.Add(this.ToString());
        }
        public override string ToString() => $"{X},{Y}";
        public void SetNewPosition(int x, int y)
        {
            PositionChangedArgs args = new PositionChangedArgs();
            if (x != X)
            {
                args.LastX = X;
                args.NewX = x;
            }
            if (y != Y)
            {
                args.LastY = Y;
                args.NewY = y;
            }
            this.x = x;
            this.y = y;
            OnPositionChanged(args);
        }
        public void SetNewPosition(string stringPosition)
        {
            var pos = stringPosition.Split(',');
            SetNewPosition(pos[0].ToInt(), pos[1].ToInt());
        }
        public void SetNewPosition(Position newPosition)
        {
            SetNewPosition(newPosition.X, newPosition.Y);
        }
        public class PositionChangedArgs
        {
            public int? LastX { get; set; }
            public int? LastY { get; set; }
            public int? NewX { get; set; }
            public int? NewY { get; set; }
            public bool XhasChanged => LastX != null && LastX != NewX;
            public bool YhasChanged => LastY != null && LastY != NewY;
        }
    }
    public class Move
    {
        private readonly string movement;
        public int X { get; }
        public int Y { get; }
        public override string ToString() => movement;
        public string Action => movement.Split(' ')[0];
        public Move(string strMove)
        {
            movement = strMove;
            var m = strMove.Split(' ');
            if (m[0] == "R") X = m[1].ToInt();
            if (m[0] == "L") X = -m[1].ToInt();
            if (m[0] == "U") Y = m[1].ToInt();
            if (m[0] == "D") Y = -m[1].ToInt();
        }

    }
    public static class Xtensions
    {


    }
}