using AdventReader;

namespace AdventOfCode2022
{
    internal class Program
    {

        static void Main(string[] args)
        {
            const int minMarker = 3; // Minimum marker quantity 4
            const int minMarkerP2 = 13; // Minimum marker quantity 14

            var signal = new Reader().ReadChars();


            // Answer 1
            Console.WriteLine($"Answer 1: {signal.DetectStartOfPacketMarker(minMarker)}");

            // Answer 2
            Console.WriteLine($"Answer 2: {signal.DetectStartOfPacketMarker(minMarkerP2)}");
        }
    }

    public static class Xtensions
    {
        public static int DetectStartOfPacketMarker(this char[] chars, int minMarker)
        {

            if (chars.Length <= minMarker) return 0;

            for (int i = minMarker; i < chars.Length; i++)
            {
                var charArr = chars.GetPreviousChars(i, minMarker);
                bool hasSameChar = false;
                foreach (char c in charArr)
                {
                    List<char> compare = charArr.ToList();
                    compare.Remove(c);
                    if (compare.Any(x=>x == c))
                    {
                        hasSameChar = true;
                        break;
                    }

                }
                if (hasSameChar) continue;
                else return i+1;
            }
            return 0;
        }
        public static char[] GetPreviousChars(this char[] chars, int currentCharIndex, int quantity)
        {
            List<char> previousChars = new List<char>();
            previousChars.Add(chars[currentCharIndex]);

            for (int i = 1; i <= quantity; i++)
            {
                int j = currentCharIndex - i;
                previousChars.Add(chars[j]);
            }

            return previousChars.ToArray();
        }
    }
}