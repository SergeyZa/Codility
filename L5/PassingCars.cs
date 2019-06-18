using System;
using System.ComponentModel.Composition;
using System.Linq;

using Solution;

namespace L5
{
    [Export(typeof(ISolution))]
    [ExportMetadata("Name", nameof(PassingCars))]
    public class PassingCars : ISolution
    {
        public int Solution(int[] A)
        {
            if (A.Length < 2)
                return 0;

            int count = 0;
            int countSoFar = 0;
            for (var i = A.Length - 1; i > -1; i--)
            {
                if (1 == A[i])
                    countSoFar++;
                else
                    count += countSoFar;
                if (count > 1000000000)
                    return -1;
            }

            return count;
        }

        public bool Test()
        {
            var tests = new[]
            {
                new
                {
                    input = "0 0",
                    expect = 0,
                },
                new
                {
                    input = "0 1",
                    expect = 1,
                },
                new
                {
                    input = "1 0",
                    expect = 0,
                },
                new
                {
                    input = "1 1",
                    expect = 0,
                },
                new
                {
                    input = "0 1 0 1 1",
                    expect = 5,
                },
            };

            bool bRet = true;
            foreach (var test in tests)
            {
                var res = Solution(test.input.Split(' ').Select(n => int.Parse(n)).ToArray());
                Console.WriteLine($"{test.input}: {test.expect} -> {res}");
                bRet = bRet && test.expect == res;
            }
            return bRet;
        }
    }
}
