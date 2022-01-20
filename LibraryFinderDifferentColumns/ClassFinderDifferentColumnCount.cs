namespace LibraryFinderDifferentColumns
{
    public class ClassFinderDifferentColumnCount
    {/// <summary>
	/// Используется для нахождения количества столбцов, значения которых различны
	/// </summary>
	/// <param name="arr">Двумерный массив</param>
	/// <returns>0 или columnCount, в зависимости от входного значения</returns>
		public static int FindDifferentColumnCount(in int[,] arr)
		{
			if (arr != null)
			{
				int columnCount = 0;
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					bool proveDifferentValues = true;
					for (int i = 0; i < arr.GetLength(0) - 1; i++)
					{
						for (int ai = i + 1; ai < arr.GetLength(0); ai++)
						{
							if (arr[i, j] == arr[ai, j])
							{
								proveDifferentValues = false;
								break;
							}
						}
						if (!proveDifferentValues) break;
					}
					if (proveDifferentValues) columnCount++;
				}
				return columnCount;
			}
			else return 0;
		}
	}
}
