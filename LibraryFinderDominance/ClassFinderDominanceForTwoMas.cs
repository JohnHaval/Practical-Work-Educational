namespace LibraryFinderDominance
{
    public class ClassFinderDominanceForTwoMas
    {
		public static int FindCellCountMoreSecondMas(in int[] firstMas, in int[] secondMas)
		{
			if (firstMas != null && secondMas != null)
			{
				int count = 0, length;
				if (firstMas.Length > secondMas.Length) length = secondMas.Length;
				else length = firstMas.Length;
				for (int i = 0; i < length; i++)
				{
					if (firstMas[i] > secondMas[i]) count++;
				}
				return count;
			}
			else return 0;
		}
	}
}
