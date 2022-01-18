using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFinderDifferentColumns
{
    public class ClassFinderDifferentColumnCount
    {
		public static int FindDifferentColumnCount(in int[,] array)
		{
			if (array != null)
			{
				int columnCount = 0;
				for (int j = 0; j < array.GetLength(1); j++)
				{
					bool proveDifferentValues = true;
					for (int i = 0; i < array.GetLength(0) - 1; i++)
					{
						for (int ai = 1; ai < array.GetLength(0); ai++)
						{
							if (array[i, j] == array[ai, j])
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
