using System.Collections.Generic;

public static class EdgeHelpers
{
	public struct Edge
	{
		public int v1;

		public int v2;

		public int triangleIndex;

		public Edge(int aV1, int aV2, int aIndex)
		{
			v1 = aV1;
			v2 = aV2;
			triangleIndex = aIndex;
		}
	}

	public static List<Edge> GetEdges(int[] aIndices)
	{
		List<Edge> list = new List<Edge>();
		for (int i = 0; i < aIndices.Length; i += 3)
		{
			int num = aIndices[i];
			int num2 = aIndices[i + 1];
			int num3 = aIndices[i + 2];
			list.Add(new Edge(num, num2, i));
			list.Add(new Edge(num2, num3, i));
			list.Add(new Edge(num3, num, i));
		}
		return list;
	}

	public static List<Edge> FindBoundary(this List<Edge> aEdges)
	{
		List<Edge> list = new List<Edge>(aEdges);
		for (int num = list.Count - 1; num > 0; num--)
		{
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				if (list[num].v1 == list[num2].v2 && list[num].v2 == list[num2].v1)
				{
					list.RemoveAt(num);
					list.RemoveAt(num2);
					num--;
					break;
				}
			}
		}
		return list;
	}

	public static List<Edge> SortEdges(this List<Edge> aEdges)
	{
		List<Edge> list = new List<Edge>(aEdges);
		for (int i = 0; i < list.Count - 2; i++)
		{
			Edge edge = list[i];
			for (int j = i + 1; j < list.Count; j++)
			{
				Edge value = list[j];
				if (edge.v2 == value.v1)
				{
					if (j != i + 1)
					{
						list[j] = list[i + 1];
						list[i + 1] = value;
					}
					break;
				}
			}
		}
		return list;
	}
}
