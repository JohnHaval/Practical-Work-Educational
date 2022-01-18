using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFinderFirstEven
{
    public class ClassFinderFirstEven
    {
        public static bool FindFirstEven(int value)
        {
            if ((value / 100) % 2 == 0) return true;
            return false;
        }
    }
}
