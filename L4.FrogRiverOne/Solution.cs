using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution;

namespace L4.FrogRiverOne
{
	[Export(typeof(ISolution))]
	[ExportMetadata("Name", nameof(L4.FrogRiverOne))]
	public class Solution : ISolution
	{
		public int solution(int X, int[] A)
		{
			if (A.Length < 2)
				return 0;

			var positions = new bool[X];

			var count = 0;

			for (var i = 0; i < A.Length; i++)
			{
				if (A[i] - 1 < X && !positions[A[i] - 1])
				{
					positions[A[i] - 1] = true;
					count++;
					if (count == X)
						return i;
				}
			}

			return -1;
		}

		public bool Test()
		{
			var tests = new[]
			{
				new {x = 5, input = new int[] { 1, 3, 1, 4, 2, 3, 5, 4 }, expect = 6 },
			};
			bool tRes = true;
			foreach (var test in tests)
			{
				var res = solution(test.x, test.input);
				Console.WriteLine($"expacting: {test.expect} result: {res}: {((test.expect == res) ? "OK" : "BAD")}");
				tRes = tRes && (test.expect == res);
			}
			return tRes;
		}
	}
}
