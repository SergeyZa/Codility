using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution;

namespace Cantaloupe.three
{
	[Export(typeof(ISolution))]
	[ExportMetadata("Name", nameof(Cantaloupe.three))]
	public class Solution : ISolution
	{
		public string solution(string S)
		{
			var res = new StringBuilder(S);

			var i = 0;
			while (i < res.Length - 1)
			{
				if ((res[i] == 'A' && res[i + 1] == 'A') ||
					(res[i] == 'B' && res[i + 1] == 'B') ||
					(res[i] == 'C' && res[i + 1] == 'C'))
				{
					res.Remove(i, 2);
					if (i > 0)
						i--;
				}
				else
					i++;
			}
			return res.ToString();
		}

		public bool Test()
		{
			var tests = new[]
			{
				new {input = "BABABA", expect = "BABABA" },
				new {input = "ACCAABBC", expect = "AC" },
				new {input = "", expect = "" },
				new {input = "B", expect = "B" },
				new {input = "ABCBBCBA", expect = "" },
				new {input = "ABCXBBBBCCBBCBBC", expect = "ABCX" },
			};
			bool tRes = true;
			foreach (var test in tests)
			{
				var res = solution(test.input);
				Console.WriteLine($"input: {test.input} expacting: {test.expect} result: {res}: {((test.expect == res) ? "OK" : "BAD")}");
				tRes = tRes && (test.expect == res);
			}
			return tRes;
		}
	}
}
