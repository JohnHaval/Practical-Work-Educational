namespace LibraryFinderFirstEven
{
    public class ClassFinderFirstEven
    {/// <summary>
    /// Используется для нахождения первой четной цифры в числе
    /// </summary>
    /// <param name="value">Число</param>
    /// <returns>true или false, в зависимоти от четности(если четная цифра - true)</returns>
        public static bool FindFirstEven(int value)
        {
            if ((value / 100) % 2 == 0) return true;
            return false;
        }
    }
}
