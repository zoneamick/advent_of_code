using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventExtensions
{
    public static class CharExtensions
    {
        public static int ToInt(this char c)
        {
            return (int)(c - '0');
        }
    }
}
