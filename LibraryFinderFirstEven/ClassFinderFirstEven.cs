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
