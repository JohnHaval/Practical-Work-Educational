namespace LibraryFinderSum
{
    public class ClassFinderSumOfMultiple
    {/// <summary>
    /// Используется для нахождения суммы чисел, которые кратны 3
    /// </summary>
    /// <param name="firstValue">Первое число</param>
    /// <param name="secondValue">Второе число</param>
    /// <param name="thirdValue">Третье число</param>
    /// <returns>sum - сумма чисел</returns>
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
