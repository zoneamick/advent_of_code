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
            Console.OutputEncoding = Encoding.UTF8;

            string puzzleKeyPart1 = "ckczppom";

            int? partOne = null;
            int? partTwo = null;
            int i = 0;
            byte[] data = null;
            string hashStr = "";

            while(true)
            {
                string currentKey = puzzleKeyPart1 + i.ToString();
                data = MD5.Create().ComputeHash(currentKey.ToByteArray());
                hashStr = data.GetMd5Hash();
                if (hashStr.StartsWith("00000") && !partOne.HasValue)
                {
                    partOne = i;
                }
                if (hashStr.StartsWith("000000"))
                {
                    partTwo = i;
                    break;
                }
                i++;
            }

            // PART ONE
            Console.WriteLine(partOne);

            // PART TWO
            Console.WriteLine(partTwo);
        }

    }


    public static class Xtensions
    {
        public static byte[] ToByteArray(this string s) => Encoding.UTF8.GetBytes(s);
        public static string GetMd5Hash(this byte[] data)
        {
            // Collect bytes
            StringBuilder sBuilder = new StringBuilder();

            // Byte to hexadecimal translation 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}