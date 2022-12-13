using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventExtensions
{
    public static class AwesomeExtensions
    {
        public static bool IsPositive(this int number) => Math.Sign(number) == 1;
        public static bool IsNegative(this int number) => Math.Sign(number) == -1;
        public static bool IsZero(this int number) => Math.Sign(number) == 0;
        public static bool IsPositiveOrZero(this int number) => Math.Sign(number) == 0 || Math.Sign(number) == 1;
        public static bool IsNegativeOrZero(this int number) => Math.Sign(number) == 0 || Math.Sign(number) == -1;
    }
}
