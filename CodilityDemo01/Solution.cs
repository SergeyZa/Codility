using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution;

namespace CodilityDemo01
{

    [Export(typeof(ISolution))]
    [ExportMetadata("Name", nameof(CodilityDemo01))]
    public class Solution : ISolution
    {
        public int Solve(int[] A)
        {
            if (A.Length == 1)
            {
                if (A[0] == 1)
                    return 2;
                return 1;
            }
            Array.Sort(A);
            if (A[A.Length - 1] <= 0)
                return 1;
            if (A[0] > 1)
                return 1;
            bool b1 = false;
            for (var i = 0; i < A.Length - 1; i++)
            {
                if (A[i] < 1)
                    continue;
                else if (A[i] == 1)
                    b1 = true;
                if (A[i + 1] - A[i] > 1)
                    return A[i] + 1;
            }
            return b1 ? A[A.Length - 1] + 1 : 1;
        }

        public bool Test()
        {
            var tests = new[]
            {
                new
                {
                    input = "1, 3, 6, 4, 1, 2",
                    expect = 5,
                },
                new
                {
                    input = "1, 2, 3",
                    expect = 4,
                },
                new
                {
                    input = "-1, -3",
                    expect = 1,
                },
                new
                {
                    input = "-1, 2",
                    expect = 1,
                },
                new
                {
                    input = "2",
                    expect = 1,
                },
            };
            bool tRes = true;

            foreach (var test in tests)
            {
                Console.WriteLine($"input: {test.input}");
                var res = Solve(test.input.Split(',').Select(v => int.Parse(v)).ToArray());
                Console.WriteLine($"output: {test.expect} expecting: {test.expect} res: {res} {res == test.expect}");
                Console.WriteLine();
                tRes = tRes && res == test.expect;
            }

            return tRes;
        }
    }
}
