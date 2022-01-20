namespace LibraryFinderDominance
{
    public class ClassFinderDominanceForTwoMas
    {/// <summary>
	/// Используется для нахождения количества ячеек первого массива, значение которых превосходит значение соответствущих ячеек второго массива
	/// </summary>
	/// <param name="firstMas">Первый массив</param>
	/// <param name="secondMas">Второй массив</param>
	/// <returns>0 или count, в зависимости от входных значений</returns>
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
