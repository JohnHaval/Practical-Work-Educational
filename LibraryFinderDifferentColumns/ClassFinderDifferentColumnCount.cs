using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFinderDifferentColumns
{
    public class ClassFinderDifferentColumnCount
    {
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
