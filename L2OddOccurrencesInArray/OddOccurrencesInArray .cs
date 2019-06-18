using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution;

namespace L2
{
    [Export(typeof(ISolution))]
    [ExportMetadata("Name", nameof(OddOccurrencesInArray))]
    public class OddOccurrencesInArray : ISolution
    {
        public int Solution(int[] A)
        {
            return A.GroupBy(a => a).Single(g => g.Count() == 1).Key;
        }

        public bool Test()
        {
            var tests = new[]
            {
                new
                {
                    input = "9 3 9 3 9 7 9",
                    expect = 7,
                },
            };
            bool bRet = true;
            foreach (var test in tests)
            {
                bRet = bRet && test.expect == Solution(test.input.Split(' ').Select(n => int.Parse(n)).ToArray());
            }
            return bRet;
        }
    }
}
