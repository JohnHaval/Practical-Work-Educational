using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFinderSum
{
    public class ClassFinderSumOfMultiple
    {
        public static int FindSumOfMultiple3(int firstValue, int secondValue, int thirdValue)
        {
            int sum = 0;
            if (firstValue % 3 == 0) sum += firstValue;
            if (secondValue % 3 == 0) sum += secondValue;
            if (thirdValue % 3 == 0) sum += thirdValue;
            return sum;
        }
    }
}
